using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model.DAL;
using Domain.Model.BLL;
using System.ComponentModel.DataAnnotations;
using SpiderServerSideWPFApp.Model.DAL;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SpiderServerSideWPFApp.Model
{
    class Service : Domain.Model.Service, INotifyPropertyChanged
    {
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

        public bool SC_ConnectionOpen
        {
            get
            {
                return SingletonSerialCommunicationDAL.ConnectionOpen;
            }
        }

        public List<string> SC_AvailableSerialPorts
        {
            get
            {
                return SingletonSerialCommunicationDAL.AvailableSerialPorts;
            }
        }

        public bool SC_IsThisSerialPortAvailable(string serialPort)
        {
            List<string> avalibleSerialPorts = SingletonSerialCommunicationDAL.AvailableSerialPorts;
            return avalibleSerialPorts.Exists(x => x == serialPort);
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
