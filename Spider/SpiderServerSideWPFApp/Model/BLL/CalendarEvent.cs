using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiderServerSideWPFApp.Model.BLL
{
    public class CalendarEvent
    {
        public CalendarEvent()
        {

        }
        public CalendarEvent(string Summary, string Location, DateTime Start, DateTime End)
        {
            this.Summary = Summary;
            this.Location = Location;
            this.Start = Start;
            this.End = End;
        }
        public string Summary { get; set; }
        public string Location { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
