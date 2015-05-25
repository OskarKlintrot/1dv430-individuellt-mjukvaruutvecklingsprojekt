using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.IO.Ports;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SpiderServerSideWPFApp.Model;
using System.Threading;
using System.ComponentModel;
using System.Windows.Threading;
using SpiderServerSideWPFApp.Model.DAL;
using Domain.Model.BLL;
using SpiderServerSideWPFApp.Model.BLL;
using System.Collections.ObjectModel;
using Microsoft.Win32;

namespace SpiderServerSideWPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields
        private DateTime dateTime;
        private Service _service;
        private ReadUpdateData _readUpdateData;
        #endregion

        #region Properties
        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }
        private ReadUpdateData ReadUpdateData
        {
            get { return _readUpdateData ?? (_readUpdateData = new ReadUpdateData()); }
        }
        private CalendarEvent[] CalendarEvents { get; set; }
        #endregion

        public MainWindow()
        {
            InitializeComponent();

            List<string> baudRateList = new List<string> { "300", "600", "1200", "2400", "4800", "9600", 
                "14400", "19200", "28800", "38400", "57600", "115200"};

            foreach (var baudRate in baudRateList)
            {
                BaudRateComboBox.Items.Add(baudRate);
            }

            addSerialPorts();

            // Set settings
            runAtStartupCheckBox.IsChecked = Properties.Settings.Default.runAtStartupSetting;
            BaudRateComboBox.Text = Properties.Settings.Default.baudRateSetting;
            if (Service.SC_IsThisSerialPortAvailable(Properties.Settings.Default.portSetting))
            {
                PortComboBox.Text = Properties.Settings.Default.portSetting;
            }

            StartButton.IsEnabled = true;
            StopButton.IsEnabled = false;
            LightOnButton.IsEnabled = false;
            LightOffButton.IsEnabled = false;
            LoadingEventsLabel.Visibility = Visibility.Hidden;
            LoadingEventsProgressBar.Visibility = Visibility.Hidden;
            DataTextBox.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;

            //var events = Service.GetEvents();

            //CalendarTextBox.Clear();
            CalendarTextBox.IsReadOnly = true;
            CalendarTextBox.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            //foreach (var item in events)
            //{
            //    CalendarTextBox.AppendText(item.Summary + ",\r" + item.Location + 
            //        "\r(" + item.Start.ToShortDateString() + " " 
            //        + item.Start.ToShortTimeString() + " - " + item.End.ToShortTimeString() + ") \r\r");
            //    CalendarTextBox.ScrollToEnd();
            //}

            //DataTextBox.Text = Service.GetEvents();

            // Run application, if run at start up enabled
            if (Properties.Settings.Default.runAtStartupSetting)
            {
                RunApplication();
            }
        }

        #region Actions
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            RunApplication();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EndConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Something went wrong when trying to end the connection to the Arduino");
            }
        }

        private void LightOnButton_Click(object sender, RoutedEventArgs e)
        {
            TurnHeatingOn();

            Room room = Service.GetRoomByID(1);
            room.Heating = true;
            Service.UpdateRoom(room);
        }

        private void LightOffButton_Click(object sender, RoutedEventArgs e)
        {
            TurnHeatingOff();

            Room room = Service.GetRoomByID(1);
            room.Heating = false;
            Service.UpdateRoom(room);
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            PortLabel.Content = "Port";
            BaudrateLabel.Content = "Baudrate";

            PortComboBox.SelectedIndex = -1;
            BaudRateComboBox.Text = "9600";

            try
            {
                if (Service.SC_ConnectionOpen)
                {
                    EndConnection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Could not reset");
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            if (Service.SC_ConnectionOpen)
            {
                EndConnection();
            }
            this.Close();
        }

        private void PortComboBox_DropDownOpened(object sender, EventArgs e)
        {
            PortComboBox.Items.Clear();
            addSerialPorts();
        }

        private void runAtStartupCheckBox_Click(object sender, RoutedEventArgs e)
        {
            var checkBox = (CheckBox)sender;
            bool value;
            if (bool.TryParse(checkBox.IsChecked.ToString(), out value))
            {
                // Save the value of the checkbox
                Properties.Settings.Default.runAtStartupSetting = value;
                Properties.Settings.Default.Save();
            }

            // Register in startup; http://stackoverflow.com/questions/11065139/launch-window-on-windows-startup
            string applicationPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var path = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
            RegistryKey key = Registry.CurrentUser.OpenSubKey(path, true);
            string applicationName = "HeatingSpiderApp";
            if (bool.TryParse(checkBox.IsChecked.ToString(), out value))
            {
                if (value)
                {
                    key.SetValue(applicationName, applicationPath);
                }
                else
                {
                    key.DeleteValue(applicationName, false);
                }
            }
        }

        private void BaudRateComboBox_DropDownClosed(object sender, EventArgs e)
        {
            Properties.Settings.Default.baudRateSetting = BaudRateComboBox.Text;
            Properties.Settings.Default.Save();
        }

        private void PortComboBox_DropDownClosed(object sender, EventArgs e)
        {
            Properties.Settings.Default.portSetting = PortComboBox.Text;
            Properties.Settings.Default.Save();
        }
        #endregion

        #region Methods
        private void RunApplication()
        {
            SetupConnectionToArduino();
            ListeningForCalendarEvents();
        }

        private void ListeningForCalendarEvents()
        {
            // Set up the UI
            LoadingEventsLabel.Visibility = Visibility.Visible;
            LoadingEventsProgressBar.Visibility = Visibility.Visible;
            LoadingEventsLabel.Content = "Laddar kalenderhändelser...";
            LoadingEventsProgressBar.Value = 100;

            // Start a background worker
            var worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            LoadingEventsProgressBar.Visibility = Visibility.Hidden;
            LoadingEventsLabel.Content = "Senast uppdaterad: " +
                DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();

            CalendarTextBox.Clear();

            CalendarEvents = e.UserState as CalendarEvent[];

            foreach (var item in CalendarEvents)
            {
                CalendarTextBox.AppendText(item.Summary + ",\r" + item.Location +
                    "\r(" + item.Start.ToShortDateString() + " "
                    + item.Start.ToShortTimeString() + " - " + item.End.ToShortTimeString() + ") \r\r");
            }
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            LoadingEventsProgressBar.Visibility = Visibility.Hidden;
            LoadingEventsLabel.Content = "Senast uppdaterad: " +
                DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();

            //CalendarTextBox.Clear();

            //CalendarEvents = e.Result as CalendarEvent[];

            //foreach (var item in CalendarEvents)
            //{
            //    CalendarTextBox.AppendText(item.Summary + ",\r" + item.Location +
            //        "\r(" + item.Start.ToShortDateString() + " "
            //        + item.Start.ToShortTimeString() + " - " + item.End.ToShortTimeString() + ") \r\r");
            //}
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;
            while (Service.SC_ConnectionOpen)
            {
                e.Result = Service.GetEvents();
                worker.ReportProgress(0, e.Result);
                Thread.Sleep(10000);
            }
        }

        private void SetupConnectionToArduino()
        {
            string port = PortComboBox.Text;
            int intBaudrate;
            int baudrate;

            try
            {
                if (int.TryParse(BaudRateComboBox.Text, out intBaudrate))
                {
                    baudrate = Convert.ToInt32(BaudRateComboBox.Text);
                }
                else
                {
                    throw new Exception("The baudrate must be a number.");
                }

                Service.SC_StartConnection(port, baudrate);

                if (Service.SC_ConnectionOpen)
                {
                    ButtonSetToStop();

                    DataTextBox.Clear();

                    PortLabel.Content = PortComboBox.Text;
                    BaudrateLabel.Content = BaudRateComboBox.Text;

                    Service.PropertyChanged += new PropertyChangedEventHandler(UpdateData);
                    Service.PropertyChanged += new PropertyChangedEventHandler(ReadData);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Something went wrong when trying to connect to the Arduino");
            }
        }

        private void addSerialPorts()
        {
            try
            {
                foreach (var port in Service.SC_AvailableSerialPorts)
                {
                    PortComboBox.Items.Add(port);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Could not find COM ports");
            }
        }

        private void EndConnection()
        {
            Service.SC_StopConnection();

            Service.PropertyChanged -= UpdateData;

            ButtonSetToStart();
        }

        private void ButtonSetToStart()
        {
            StartButton.IsEnabled = true;
            StopButton.IsEnabled = false;
            LightOnButton.IsEnabled = false;
            LightOffButton.IsEnabled = false;
            PortComboBox.IsEnabled = true;
            BaudRateComboBox.IsEnabled = true;
        }

        private void ButtonSetToStop()
        {
            StartButton.IsEnabled = false;
            StopButton.IsEnabled = true;
            LightOnButton.IsEnabled = true;
            LightOffButton.IsEnabled = false;
            PortComboBox.IsEnabled = false;
            BaudRateComboBox.IsEnabled = false;
        }

        private void TurnHeatingOn()
        {
            try
            {
                Service.SC_SendData("1H#");

                LightOnButton.Dispatcher.Invoke(new Action(() => LightOnButton.IsEnabled = false), DispatcherPriority.Normal, null);
                LightOffButton.Dispatcher.Invoke(new Action(() => LightOffButton.IsEnabled = true), DispatcherPriority.Normal, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Could not send data.");
            }
        }

        private void TurnHeatingOff()
        {
            try
            {
                Service.SC_SendData("1L#");

                LightOnButton.Dispatcher.Invoke(new Action(() => LightOnButton.IsEnabled = true), DispatcherPriority.Normal, null);
                LightOffButton.Dispatcher.Invoke(new Action(() => LightOffButton.IsEnabled = false), DispatcherPriority.Normal, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Could not send data.");
            }
        } 
        #endregion

        #region Events
        private void ReadData(object sender, PropertyChangedEventArgs e)
        {
            Room[] room = ReadUpdateData.ReadData(6);

            if (room[0].Heating)
            {
                LightOnButton.Dispatcher.Invoke(new Action(() => LightOnButton.IsEnabled = false), DispatcherPriority.Normal, null);
                LightOffButton.Dispatcher.Invoke(new Action(() => LightOffButton.IsEnabled = true), DispatcherPriority.Normal, null);
            }
            else
            {
                LightOnButton.Dispatcher.Invoke(new Action(() => LightOnButton.IsEnabled = true), DispatcherPriority.Normal, null);
                LightOffButton.Dispatcher.Invoke(new Action(() => LightOffButton.IsEnabled = false), DispatcherPriority.Normal, null);
            }
        }
        
        private void UpdateData(object sender, PropertyChangedEventArgs e)
        {
            string ReceivedData = Service.SC_ReceivedData;

            // Update the DataTextBox
            dateTime = DateTime.Now;
            string timeStamp = dateTime.ToString("HH:mm:ss");
            string stringToInsert = timeStamp + "\t\t" + Service.SC_ReceivedData + "\n";
            DataTextBox.Dispatcher.Invoke(new Action(() => DataTextBox.AppendText(stringToInsert)), DispatcherPriority.Normal, null);
            DataTextBox.Dispatcher.Invoke(new Action(() => DataTextBox.ScrollToEnd()), DispatcherPriority.Normal, null);

            ReadUpdateData.UpdateData(ReceivedData);
        }
        #endregion
    }
}
