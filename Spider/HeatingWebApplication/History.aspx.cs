using HeatingWebApplication.Models;
using HeatingWebApplication.Models.BLL;
using Domain.Model.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;

namespace HeatingWebApplication
{
    public partial class History : System.Web.UI.Page
    {
        private Service _service;

        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        [WebMethod]
        public static string GetChartData(int[] roomID)
        {
            var Service = new Service();
            var dataToParse = new HistoricalData[roomID.Length];
            for (int i = 0; i < dataToParse.Length; i++)
            {
                dataToParse[i] = new HistoricalData();
            }

            for (int i = 0; i < roomID.Length; i++)
            {
                try
                {
                    // Add room description
                    var tempRoomDescription = new Room();
                    tempRoomDescription = Service.GetRoomByID(roomID[i]);
                    dataToParse[i].RoomDescription = tempRoomDescription.RoomDescription;

                    // Add timestamp and temperatures
                    IEnumerable<Temperature> tempHistory = Service.GetTemperaturesByRoomID(roomID[i]);

                    var tempHistoryArray = tempHistory.ToArray();
                
                    dataToParse[i].Temperatures = new int[tempHistoryArray.Length];
                    dataToParse[i].Timestamp = new string[tempHistoryArray.Length];

                    for (int j = 0; j < tempHistoryArray.Length; j++)
			        {
                        dataToParse[i].Temperatures[j] = tempHistoryArray[j].Temp;
                        dataToParse[i].Timestamp[j] = tempHistoryArray[j].Timestamp.ToString();
			        }
                }
                catch (Exception)
                {
                    return null;
                }
            }

            var context = new JavaScriptSerializer().Serialize(dataToParse);

            return context;
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
    }
}