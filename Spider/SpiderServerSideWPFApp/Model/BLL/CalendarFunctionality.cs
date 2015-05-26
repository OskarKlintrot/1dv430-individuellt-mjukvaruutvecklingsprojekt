using Domain.Model.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiderServerSideWPFApp.Model.BLL
{
    public class CalendarFunctionality
    {
        #region Fields
        private Service _service;
        #endregion

        #region Properties
        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }
        #endregion
        #region Methods
        public void UpdateDbWithCalendarEvents(CalendarEvent[] CalendarEvents, int startHeatingInAdvancedHours, int stopHeatingMinutes)
        {
            DateTime startHeating = DateTime.Now.AddHours(startHeatingInAdvancedHours);
            DateTime stopHeating = DateTime.Now.AddMinutes(stopHeatingMinutes);

            IEnumerable<Room> Rooms = Service.GetRooms();
            Room[] RoomArray = Rooms.ToArray();

            // Make location and room description lower case
            for (int i = 0; i < RoomArray.Length; i++)
            {
                RoomArray[i].RoomDescription = RoomArray[i].RoomDescription.ToLower();
            }
            for (int i = 0; i < CalendarEvents.Length; i++)
            {
                CalendarEvents[i].Location = CalendarEvents[i].Location.ToLower();
            }

            foreach (var item in RoomArray)
            {
                for (int i = 0; i < CalendarEvents.Length; i++)
                {
                    if (CalendarEvents[i].Location.Contains(item.RoomDescription))
                    {
                        bool oldHeating = item.Heating;
                        
                        // See if heating should be on or off
                        if (CalendarEvents[i].End < stopHeating)
                        {
                            item.Heating = false;
                        }
                        else if (CalendarEvents[i].Start < startHeating)
                        {
                            item.Heating = true;
                        }

                        // See if heating needs to be changed
                        if (item.Heating != oldHeating)
                        {
                            // Update heating in DB
                            Service.UpdateRoom(item);
                        }
                    }
                }
            }
        }
        #endregion
    }
}
