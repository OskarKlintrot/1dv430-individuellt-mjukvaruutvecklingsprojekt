using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SpiderServerSideWPFApp.Model
{
    class SerialCommunication : INotifyPropertyChanged
    {
        private string _receivedData;
        private SerialPort MyPort;

        public event PropertyChangedEventHandler PropertyChanged;

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

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (!string.IsNullOrWhiteSpace(propertyName) && PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void Start_Connection(string port, int baudrate )
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Something went wrong when trying to connect to the Arduino");
            }
        }

        public void Stop_Connection()
        {
            try
            {
                MyPort.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Could not stop the connection.");
            }
        }

        private void myport_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            ReceivedData = MyPort.ReadLine();
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
    }
}
