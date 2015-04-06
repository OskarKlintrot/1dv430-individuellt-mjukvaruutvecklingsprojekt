using Spider.Model.BLL;
using Spider.Model.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spider.Model
{
    class Service
    {
        #region Temperature

        private TemperatureDAL _temperatureDAL;

        private TemperatureDAL TemperatureDAL
        {
            get { return _temperatureDAL ?? (_temperatureDAL = new TemperatureDAL()); }
        }

        public IEnumerable<Temperature> GetTemperatures()
        {
            return TemperatureDAL.GetTemperatures();
        }

        public void InsertTemperature(Temperature temperature)
        {
            TemperatureDAL.InsertTemperature(temperature);
        }

        #endregion

        #region Room

        private RoomDAL _roomDAL;

        private RoomDAL RoomDAL
        {
            get { return _roomDAL ?? (_roomDAL = new RoomDAL()); }
        }

        public IEnumerable<Room> GetRooms()
        {
            return RoomDAL.GetRooms();
        }

        public void UpdateRoom(Room room)
        {
            RoomDAL.UpdateRoom(room);
        }

        #endregion
    }
}
