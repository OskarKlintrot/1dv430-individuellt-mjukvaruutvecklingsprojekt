using Spider.Model.BLL;
using Spider.Model.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            ICollection<ValidationResult> validationResults;
            if (!temperature.Validate(out validationResults))
            {
                throw new AggregateException("Objektet klarade inte valideringen.",
                    validationResults.Select(vr => new ValidationException(vr.ErrorMessage)).ToList().AsReadOnly());
            }

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
            ICollection<ValidationResult> validationResults;
            if (!room.Validate(out validationResults))
            {
                throw new AggregateException("Objektet klarade inte valideringen.",
                    validationResults.Select(vr => new ValidationException(vr.ErrorMessage)).ToList().AsReadOnly());
            }

            RoomDAL.UpdateRoom(room);
        }

        #endregion
    }
}
