using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HeatingWebApplication.Models.BLL
{
    public class ProcessedHistoricalData
    {
        public string[] Timestamp { get; set; }
        public HistoricalDataShort[] Room { get; set; }
    }
}