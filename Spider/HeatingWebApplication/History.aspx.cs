using HeatingWebApplication.Models;
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
            if (roomID.Length == 0)
            {
                throw new Exception("Minst ett rum måste anges!");
            }

            var Service = new Service();
            var UtilityLibrary = new UtilityLibrary();

            // Prepare an object to contain the data from database
            var rawHistory = new RawHistory[roomID.Length];
            for (int i = 0; i < rawHistory.Length; i++)
            {
                rawHistory[i] = new RawHistory();
            }
            for (int i = 0; i < rawHistory.Length; i++)
            {
                rawHistory[i].TemperatureAndTimestamp = new List<RawData>();
            }

            // Get all data from database and store it in the previously created object
            for (int i = 0; i < roomID.Length; i++)
            {
                try
                {
                    // Add room description
                    var tempRoomDescription = new Domain.Model.BLL.Room();
                    tempRoomDescription = Service.GetRoomByID(roomID[i]);
                    rawHistory[i].RoomDescription = tempRoomDescription.RoomDescription;

                    // Add timestamp and temperatures
                    IEnumerable<Domain.Model.BLL.Temperature> tempHistory = Service.GetTemperaturesByRoomIDAndDate(roomID[i], startDate, endDate);
                    var tempHistoryArray = tempHistory.ToArray();
                    var lengthOfArray = tempHistoryArray.Length / scale;
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
                }
                catch (Exception ex)
                {
                    throw new Exception("Ett fel uppstod vid hämtning av historiken; " + ex.Message);
                }
            }

            // Refactor the object to remove unnecessary data and duplicates and make it JSON-friendly
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
            tbx.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
        }

        protected void EndDateTextBox_Load(object sender, EventArgs e)
        {
            TextBox tbx = (TextBox)sender;
            tbx.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }
}