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

            foreach (var item in RoomArray)
            {
                bool roomInUse = false;
                if (CalendarEvents.Length > 0)
                {
                    for (int i = 0; i < CalendarEvents.Length; i++)
                    {
                        // Make location and room description lower case
                        string tempEventLocation = CalendarEvents[i].Location.ToLower();
                        string tempRoomDescription = item.RoomDescription.ToLower();

                        if (tempEventLocation.Contains(tempRoomDescription) && item.AutomaticControl)
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
                            else if (!roomInUse)
                            {
                                item.Heating = false;
                            }
                            // Make sure we only set the heating according to the first and thereby the earliest event
                            roomInUse = true;

                            UpdateRoom(oldHeating, item.Heating, item);
                        }
                        else if (!roomInUse)
                        {
                            TurnHeatingOff(item);
                        }
                    } 
                }
                else
                {
                    // If no events, turn all rooms off
                    TurnHeatingOff(item);
                }
            }
        }

        private void UpdateRoom(bool oldHeating, bool newHeating, Room room)
        {
            // See if heating needs to be changed
            if (newHeating != oldHeating)
            {
                // Update heating in DB
                Service.UpdateRoom(room);
                Console.WriteLine("{0} updaterat till {1}", room.RoomDescription, room.Heating);
            }
        }

        private void TurnHeatingOff(Room room)
        {
            var oldHeating = room.Heating;
            room.Heating = false;
            UpdateRoom(oldHeating, room.Heating, room);
        }
        #endregion
    }
}
