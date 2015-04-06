using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spider.Model.BLL
{
    class Temperature
    {
        public int TempID { get; set; }
        public DateTime? Timestamp { get; set; }
        public int RoomID { get; set; }
        public int Temp { get; set; }
    }
}
