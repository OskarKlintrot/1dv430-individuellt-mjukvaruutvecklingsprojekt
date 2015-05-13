using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Model.BLL;

namespace HeatingWebApplication.Models.BLL
{
    public class UtilityLibrary : Domain.Model.BLL.UtilityLibrary
    {
        public ProcessedHistoricalData BreakOutTimestampFromRawHistory(RawHistory[] rh, int scale)
        {
            // Prepeare a ProcessedHistoricalData object
            var history = new ProcessedHistoricalData();
            history.Room = new HistoricalDataShort[rh.Length];
            for (int i = 0; i < history.Room.Length; i++)
            {
                history.Room[i] = new HistoricalDataShort();
            }

            var rawHistory = new RawHistory[rh.Length];

            // Round every timestamp up to nearest 10 minutes
            for (int i = 0; i < rh.Length; i++)
            {
                for (int j = 0; j < rh[i].TemperatureAndTimestamp.Count; j++)
                {
                    rh[i].TemperatureAndTimestamp[j].TimeStamp = RoundUpDateTime(rh[i].TemperatureAndTimestamp[j].TimeStamp, scale);
                }
                // Remove all duplicates
                rh[i].TemperatureAndTimestamp = rh[i].TemperatureAndTimestamp.Distinct(new RawDataEqualityComparer()).ToList();
                rh[i].TemperatureAndTimestamp.TrimExcess();
            }

            // Merge the timestamps to one array
            List<DateTime> tempTimeStamp = new List<DateTime>();
            List<string> stringTimeStamp = new List<string>();
            for (int i = 0; i < rh.Length; i++)
            {
                for (int j = 0; j < rh[i].TemperatureAndTimestamp.Count; j++)
                {
                    tempTimeStamp.Add(rh[i].TemperatureAndTimestamp[j].TimeStamp); 
                }
            }

            // Remove all duplicates
            tempTimeStamp = tempTimeStamp.Distinct().ToList();
            tempTimeStamp.Sort();
            tempTimeStamp.TrimExcess();

            // Add a new array with the correct number of timestamps
            history.Timestamp = new string[tempTimeStamp.Count];

            foreach (var item in tempTimeStamp)
            {
                stringTimeStamp.Add(item.ToString());
            }

            stringTimeStamp.TrimExcess();
            
            history.Timestamp = stringTimeStamp.ToArray();

            // Add room descriptions
            for (int i = 0; i < rh.Length; i++)
            {
                history.Room[i].RoomDescription = rh[i].RoomDescription;
            }

            // Add room temperatures
            for (int i = 0; i < rh.Length; i++)
            {
                history.Room[i].Temperatures = new int?[history.Timestamp.Length];
            }
            
            int index;
            List<DateTime> finalTimeStamp = history.Timestamp.Select(date => DateTime.Parse(date)).ToList();
            finalTimeStamp.TrimExcess();
            DateTime[] localTimeStamp;
            for (int i = 0; i < rh.Length; i++)
			{
                // Prepare
                localTimeStamp = new DateTime[rh[i].TemperatureAndTimestamp.Count];
                
                // Copy
                for (int j = 0; j < rh[i].TemperatureAndTimestamp.Count; j++)
                {
                    localTimeStamp[j] = rh[i].TemperatureAndTimestamp[j].TimeStamp;
                }

                // Work
                for (int j = 0; j < finalTimeStamp.Count; j++)
			    {
                    index = Array.FindIndex(localTimeStamp, item => item == finalTimeStamp[j]);
                    if (j < rh[i].TemperatureAndTimestamp.Count && index >= 0)
                        history.Room[i].Temperatures[index] = rh[i].TemperatureAndTimestamp[j].Temperature;
                    else if (j < rh[i].TemperatureAndTimestamp.Count && index < 0)
                        history.Room[i].Temperatures[j] = null;
			    }
                
            }

            return history;
        }

        private class RawDataEqualityComparer : IEqualityComparer<RawData>
        {

            public bool Equals(RawData x, RawData y)
            {
                return x.TimeStamp == y.TimeStamp;
            }

            public int GetHashCode(RawData obj)
            {
                return obj.TimeStamp.GetHashCode();
            }
        }
    }
}