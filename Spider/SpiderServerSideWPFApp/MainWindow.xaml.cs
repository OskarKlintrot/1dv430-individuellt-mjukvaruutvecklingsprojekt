﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
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
using System.Text.RegularExpressions;
using SpiderServerSideWPFApp.Model.BLL;

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
        private bool[] oldHeating = new bool[6];
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

        #endregion

        public MainWindow()
        {
            InitializeComponent();

            StartButton.IsEnabled = true;
            StopButton.IsEnabled = false;
            LightOnButton.IsEnabled = false;
            LightOffButton.IsEnabled = false;
            DataTextBox.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
        }

        #region Actions
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            string port = PortTextBox.Text;
            int intBaudrate;
            int baudrate;

            try
            {
                if (int.TryParse(BaudrateTextBox.Text, out intBaudrate))
                {
                    baudrate = Convert.ToInt32(BaudrateTextBox.Text);
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

                    PortLabel.Content = PortTextBox.Text;
                    BaudrateLabel.Content = BaudrateTextBox.Text;

                    Service.PropertyChanged += new PropertyChangedEventHandler(UpdateData);
                    Service.PropertyChanged += new PropertyChangedEventHandler(ReadData);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Something went wrong when trying to connect to the Arduino");
            }
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

            PortTextBox.Text = "COM4";
            BaudrateTextBox.Text = "9600";

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
        #endregion

        #region Methods
        private void EndConnection()
        {
            Service.SC_StopConnection();

            Service.PropertyChanged -= UpdateData;

            ButtonSetToStart();

            for (int i = 0; i < oldHeating.Length; i++)
            {
                oldHeating[i] = false; 
            }
        }

        private void ButtonSetToStart()
        {
            StartButton.IsEnabled = true;
            StopButton.IsEnabled = false;
            LightOnButton.IsEnabled = false;
            LightOffButton.IsEnabled = false;
            PortTextBox.IsEnabled = true;
            BaudrateTextBox.IsEnabled = true;
        }

        private void ButtonSetToStop()
        {
            StartButton.IsEnabled = false;
            StopButton.IsEnabled = true;
            LightOnButton.IsEnabled = true;
            LightOffButton.IsEnabled = false;
            PortTextBox.IsEnabled = false;
            BaudrateTextBox.IsEnabled = false;
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
