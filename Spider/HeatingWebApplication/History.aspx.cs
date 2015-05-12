﻿using HeatingWebApplication.Models;
using HeatingWebApplication.Models.BLL;
//using Domain.Model.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.Web.Script.Services;

namespace HeatingWebApplication
{
    public partial class History : System.Web.UI.Page
    {
        private Service _service;
        private UtilityLibrary _utilityLibrary;

        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }
        private UtilityLibrary UtilityLibrary
        {
            get { return _utilityLibrary ?? (_utilityLibrary = new UtilityLibrary()); }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        [WebMethod]
        public static ProcessedHistoricalData GetChartData(int[] roomID, DateTime startDate, DateTime endDate, int scale)
        {
            var Service = new Service();
            var UtilityLibrary = new UtilityLibrary();
            // EXPERIMENTAL
            var rawHistory = new RawHistory[roomID.Length];
            //var historicalReadings = new HistoricalDataLong[roomID.Length];
            //for (int i = 0; i < historicalReadings.Length; i++)
            //{
            //    historicalReadings[i] = new HistoricalDataLong();
            //}

            // EXPERIMENTAL
            for (int i = 0; i < rawHistory.Length; i++)
            {
                rawHistory[i] = new RawHistory();
            }
            for (int i = 0; i < rawHistory.Length; i++)
            {
                rawHistory[i].TemperatureAndTimestamp = new List<RawData>();
            }

            for (int i = 0; i < roomID.Length; i++)
            {
                try
                {
                    // Add room description
                    var tempRoomDescription = new Domain.Model.BLL.Room();
                    tempRoomDescription = Service.GetRoomByID(roomID[i]);
                    //historicalReadings[i].RoomDescription = tempRoomDescription.RoomDescription;
                    
                    // EXPERIMENTAL
                    rawHistory[i].RoomDescription = tempRoomDescription.RoomDescription;

                    // Add timestamp and temperatures
                    //IEnumerable<Temperature> tempHistory = Service.GetTemperaturesByRoomID(roomID[i]);
                    IEnumerable<Domain.Model.BLL.Temperature> tempHistory = Service.GetTemperaturesByRoomIDAndDate(roomID[i], startDate, endDate);

                    var tempHistoryArray = tempHistory.ToArray();
                    var lengthOfArray = tempHistoryArray.Length / scale;

                    //historicalReadings[i].Temperatures = new int[lengthOfArray];
                    //historicalReadings[i].Timestamp = new string[lengthOfArray];

                    // EXPERIMENTAL
                    var rawData = new RawData[tempHistoryArray.Length];
                    for (int j = 0; j < rawData.Length; j++)
                    {
                        rawData[j] = new RawData();
                    }
                    
                    for (int j = 0; j < tempHistoryArray.Length; j++)
			        {
			            rawData[j].Temperature = tempHistoryArray[j].Temp;
                        rawData[j].TimeStamp = DateTime.Parse(tempHistoryArray[j].Timestamp.ToString());
			        }
                    for (int j = 0; j < tempHistoryArray.Length; j++)
                    {
                        rawHistory[i].TemperatureAndTimestamp.Add(rawData[j]);
                    }

                    //for (int j = 0; j < lengthOfArray; j++)
                    //{
                    //    historicalReadings[i].Temperatures[j] = tempHistoryArray[j * scale].Temp;
                    //    historicalReadings[i].Timestamp[j] = tempHistoryArray[j * scale].Timestamp.ToString();
                    //}
                }
                catch (Exception ex)
                {
                    // TODO: Send back better error message
                    //historicalReadings[0].RoomDescription = ex.Message.ToString();
                    //return historicalReadings;
                }
            }

            var historicalReadings = UtilityLibrary.BreakOutTimestampFromRawHistory(rawHistory, scale);
            
            return historicalReadings;
        }

        // The return type can be changed to IEnumerable, however to support
        // paging and sorting, the following parameters must be added:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IEnumerable<Domain.Model.BLL.Room> AvailableRoomsListView_GetData()
        {
            try
            {
                return Service.GetRooms();
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "Ett fel inträffande vid hämtning av rummen från databasen.");
                return null;
            }
        }

        protected void StartDateTextBox_Load(object sender, EventArgs e)
        {
            TextBox tbx = (TextBox)sender;
            tbx.Text = DateTime.Now.AddMonths(-1).ToShortDateString();
        }

        protected void EndDateTextBox_Load(object sender, EventArgs e)
        {
            TextBox tbx = (TextBox)sender;
            tbx.Text = DateTime.Now.ToShortDateString();
        }
    }
}