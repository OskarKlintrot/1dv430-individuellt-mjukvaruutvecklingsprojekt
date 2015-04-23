using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SpiderServerSideWPFApp.Model.DAL
{
    public sealed class SingletonSerialCommunicationDAL : INotifyPropertyChanged
    {
        // Singleton pattern - thread-safe without using locks
        // http://csharpindepth.com/Articles/General/Singleton.aspx
        #region Singleton Pattern
        private static readonly SingletonSerialCommunicationDAL instance = new SingletonSerialCommunicationDAL();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static SingletonSerialCommunicationDAL()
        {
        }

        private SingletonSerialCommunicationDAL()
        {
        }

        public static SingletonSerialCommunicationDAL Instance
        {
            get
            {
                return instance;
            }
        } 
        #endregion

        // Serial communication
        #region Fields
        private string _receivedData;
        private SerialPort MyPort;
        #endregion

        #region Properties
        //True if the connection is open, false if not
        public bool ConnectionOpen { get; private set; }
        public string ReceivedData
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
        public static List<string> AvailableSerialPorts
        {
            get
            {
                List<string> list = new List<string>(SerialPort.GetPortNames());
                return list;
            }
        }
        #endregion

        #region OnPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (!string.IsNullOrWhiteSpace(propertyName) && PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        } 
        #endregion

        #region Methods
        public void StartConnection(string port, int baudrate)
        {
            try
            {
                MyPort = new SerialPort();
                MyPort.BaudRate = baudrate;
                MyPort.PortName = port;
                MyPort.Parity = Parity.None;
                MyPort.DataBits = 8;
                MyPort.StopBits = StopBits.One;
                MyPort.DataReceived += myport_DataReceived;
                // TODO: Implement error message
                //myport.ErrorReceived += myport_ErrorReceived;
                MyPort.Open();
                ConnectionOpen = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Something went wrong when trying to connect to the Arduino");
            }
        }
        public void StopConnection()
        {
            try
            {
                MyPort.DataReceived -= myport_DataReceived;
                MyPort.Close();
                ConnectionOpen = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Could not stop the connection.");
            }
        }
        public void SendDataOverSerial(string dataToSend)
        {
            try
            {
                MyPort.WriteLine(dataToSend);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Could not send data to the Arduino");
            }
        } 
        #endregion

        #region Events
        private void myport_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            ReceivedData = MyPort.ReadLine();
        }
        #endregion
    }
}
