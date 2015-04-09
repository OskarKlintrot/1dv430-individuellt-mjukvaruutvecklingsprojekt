using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.BLL
{
    public class Temperature : BusinessObjectBase
    {
        public int TempID { get; set; }
        [DataType(DataType.Date, ErrorMessage = "Datumet måste vara i rätt format!")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime? Timestamp { get; set; }
        public int RoomID { get; set; }
        public int Temp { get; set; }
    }
}
