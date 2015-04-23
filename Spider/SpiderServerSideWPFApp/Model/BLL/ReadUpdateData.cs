using Domain.Model.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace SpiderServerSideWPFApp.Model.BLL
{
    class ReadUpdateData
    {
        private bool startUp = true;
        private bool[] oldHeating = new bool[6];
        private Service _service;
        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }
        public void UpdateData(string ReceivedData)
        {
            // Split recived port into array and "clean" it
            string[] tempStringArray = ReceivedData.Split('#');
            string[] cleanedTempString = new String[tempStringArray.Length];

            for (int i = 0; i < tempStringArray.Length; i++)
            {
                try
                {
                    Regex regexObj = new Regex(@"[^\d]");
                    cleanedTempString[i] = regexObj.Replace(tempStringArray[i], "");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            // Insert recived port to databas
            for (int i = 0; i < cleanedTempString.Length; i++)
            {
                int roomID;
                int temp;

                if (cleanedTempString[i].Length > 0)
                {
                    if ((Int32.TryParse(cleanedTempString[i][0].ToString(), out roomID))
                    && (Int32.TryParse(cleanedTempString[i].Substring(1), out temp)))
                    {
                        Temperature temperature = new Temperature
                        {
                            TempID = 0,
                            RoomID = roomID,
                            Temp = temp
                        };

                        try
                        {
                            Service.InsertTemperature(temperature);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString(), "Ett fel inträffande vid sparande i databasen");
                        }
                    }
                }
            }
        }

        public Room[] ReadData(int numberOfRooms)
        {
            Room[] room = new Room[6];
            bool[] newHeating = new bool[room.Length];

            for (int i = 0; i < room.Length; i++)
            {
                room[i] = Service.GetRoomByID(i + 1);
                newHeating[i] = room[i].Heating;
            }

            for (int i = 0; i < newHeating.Length; i++)
            {
                if ((newHeating[i] != oldHeating[i]) || startUp)
                {
                    switch (newHeating[i])
                    {
                        case true:
                            Service.SC_SendData(i + 1 + "H#");
                            break;
                        case false:
                            Service.SC_SendData(i + 1 + "L#");
                            break;
                        default:
                            break;
                    }
                    oldHeating[i] = newHeating[i];
                }
            }
            if (startUp)
            {
                startUp = false;
            }
            return room;
        }
    }
}
