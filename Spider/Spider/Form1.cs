//This application builds upon the examples Chatchai Buekban published
//on his youtube-channel; https://www.youtube.com/user/Buekban/videos

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace Spider
{
    public partial class Form1 : Form
    {
        private SerialPort myport;
        private DateTime dateTime;
        private string receivedData;
        public Form1()
        {
            InitializeComponent();
            init();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void On_btn_Click(object sender, EventArgs e)
        {
            SendDataOverSerial("HIGH");

            On_btn.Enabled = false;
            Off_btn.Enabled = true;
        }

        private void Off_btn_Click(object sender, EventArgs e)
        {
            SendDataOverSerial("LOW");

            On_btn.Enabled = true;
            Off_btn.Enabled = false;
        }
            private void init()
        {
            On_btn.Enabled = false;
            Off_btn.Enabled = false;
            Start_btn.Enabled = true;
            Stop_btn.Enabled = false;
        }

        private void Start_btn_Click(object sender, EventArgs e)
        {
            Start_Connection();
        }

        private void myport_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            receivedData = myport.ReadLine();
            
            this.Invoke(new EventHandler(displayDataEvent));
        }

        private void displayDataEvent(object sender, EventArgs e)
        {
            dateTime = DateTime.Now;
            string timeStamp = dateTime.ToString("HH:mm:ss");
            DataTextBox.AppendText(timeStamp + "\t\t" + receivedData + "\n");

            int intReceived;
            if (int.TryParse(receivedData, out intReceived))
            {
                ReadingProgressBar.Value = intReceived; 
            }
        }

        private void Stop_btn_Click(object sender, EventArgs e)
        {
            Stop_Connection();
        }

        private void Reset_btn_Click(object sender, EventArgs e)
        {
            PortLabel.Text = "Port";
            BaudrateLabel.Text = "Baudrate";

            PortTextBox.Text = "COM4";
            BaudrateTextBox.Text = "9600";

            Stop_Connection();
        }

        private void Start_Connection()
        {
            try
            {
                myport = new SerialPort();
                myport.BaudRate = Convert.ToInt32(BaudrateTextBox.Text);
                myport.PortName = PortTextBox.Text;
                myport.Parity = Parity.None;
                myport.DataBits = 8;
                myport.StopBits = StopBits.One;
                myport.DataReceived += myport_DataReceived;
                myport.ErrorReceived += myport_ErrorReceived;
                myport.Open();

                Start_btn.Enabled = false;
                Stop_btn.Enabled = true;
                On_btn.Enabled = true;
                DataTextBox.Text = "";

                PortLabel.Text = myport.PortName;
                BaudrateLabel.Text = myport.BaudRate.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Something went wrong when trying to connect to the Arduino");
            }
        }

        private void myport_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            MessageBox.Show("An error occurred");
        }

        private void Stop_Connection()
        {
            try
            {
                myport.Close();

                Start_btn.Enabled = true;
                Stop_btn.Enabled = false;
                On_btn.Enabled = false;
                Off_btn.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Could not stop the connection.");
            }
        }

        private void SendDataOverSerial(string dataToSend)
        {
            try
            {
                myport.WriteLine(dataToSend);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Could not send data to the Arduino");
            }
        }
    }
}
