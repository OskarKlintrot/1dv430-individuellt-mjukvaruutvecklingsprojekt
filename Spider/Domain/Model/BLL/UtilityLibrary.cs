using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.BLL
{
    public class UtilityLibrary
    {
        /// <summary>
        /// Solution from http://stackoverflow.com/questions/7029353/how-can-i-round-up-the-time-to-the-nearest-x-minutes
        /// </summary>
        /// <param name="dt">DateTime to be rounded up...</param>
        /// <param name="timeSpanInMinutes">...to nearest time span given in minutes</param>
        /// <returns>The new, rounded, DateTime</returns>
        public DateTime RoundUpDateTime(DateTime dt, int timeSpanInMinutes)
        {
            var ts = TimeSpan.FromMinutes(timeSpanInMinutes);
            return new DateTime(((dt.Ticks + ts.Ticks - 1) / ts.Ticks) * ts.Ticks);
        }
    }
}
