using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model.DAL;
using Domain.Model.BLL;
//using System.Windows.Controls;

using System.ComponentModel.DataAnnotations;
using SpiderServerSideWPFApp.Model.DAL;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SpiderServerSideWPFApp.Model
{
    class Service : Domain.Model.Service, INotifyPropertyChanged
    {
        //#region Temperature

        //private TemperatureDAL _temperatureDAL;

        //private TemperatureDAL TemperatureDAL
        //{
        //    get { return _temperatureDAL ?? (_temperatureDAL = new TemperatureDAL()); }
        //}

        //public IEnumerable<Temperature> GetTemperatures()
        //{
        //    return TemperatureDAL.GetTemperatures();
        //}

        //public void InsertTemperature(Temperature temperature)
        //{
        //    ICollection<ValidationResult> validationResults;
        //    if (!temperature.Validate(out validationResults))
        //    {
        //        throw new Exception("Objektet klarade inte valideringen.");
        //    }

        //    TemperatureDAL.InsertTemperature(temperature);
        //}

        //#endregion

        //#region Room

        //private RoomDAL _roomDAL;

        //private RoomDAL RoomDAL
        //{
        //    get { return _roomDAL ?? (_roomDAL = new RoomDAL()); }
        //}

        //public IEnumerable<Room> GetRooms()
        //{
        //    return RoomDAL.GetRooms();
        //}

        //public void UpdateRoom(Room room)
        //{
        //    ICollection<ValidationResult> validationResults;
        //    if (!room.Validate(out validationResults))
        //    {
        //        throw new Exception("Objektet klarade inte valideringen.");
        //    }

        //    RoomDAL.UpdateRoom(room);
        //}

        //#endregion

        #region Serial Communication

        private SingletonSerialCommunicationDAL SingletonSerialCommunicationDAL = SingletonSerialCommunicationDAL.Instance;

        public void SC_StartConnection(string port, int baudrate)
        {
            SingletonSerialCommunicationDAL.StartConnection(port, baudrate);

            SingletonSerialCommunicationDAL.PropertyChanged += new PropertyChangedEventHandler(UpdateReceivedData);
        }

        private void UpdateReceivedData(object sender, PropertyChangedEventArgs e)
        {
            SC_ReceivedData = SingletonSerialCommunicationDAL.ReceivedData;
        }

        public void SC_StopConnection()
        {
            SingletonSerialCommunicationDAL.StopConnection();
        }

        public void SC_SendData(string data)
        {
            SingletonSerialCommunicationDAL.SendDataOverSerial(data);
        }

        // Notify on change
        private string _receivedData;

        public event PropertyChangedEventHandler PropertyChanged;

        public string SC_ReceivedData
        {
            get
            {
                return _receivedData;
            }
            set
            {
                if (_receivedData != value)
                {
                    _receivedData = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (!string.IsNullOrWhiteSpace(propertyName) && PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
