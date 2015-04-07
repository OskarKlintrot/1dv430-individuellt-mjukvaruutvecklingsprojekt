using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spider.Model.BLL
{
    class Room : BusinessObjectBase
    {
        public int RoomID { get; set; }
        [Required(ErrorMessage = "En beskrivning av rummet måste anges.")]
        [StringLength(50, ErrorMessage = "Rumsnamnet får vara max 50 tecken.")]
        public string RoomDescription { get; set; }
        [Required(ErrorMessage = "Värme till eller från måste anges.")]
        public bool Heating { get; set; }
    }
}
