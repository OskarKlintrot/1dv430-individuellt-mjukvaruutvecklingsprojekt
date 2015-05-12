using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HeatingWebApplication.Models.BLL
{
    public class HistoricalDataShort
    {
        public string RoomDescription { get; set; }
        public int?[] Temperatures { get; set; }
    }
}