using System;
using System.Web;

namespace HandymanTools.Common.Models
{
    public class ReservationTool
    {
        public int ReservationNumber { get; set; }
        public Reservation Reservation { get; set; }

        public int ToolId { get; set; }
        public Tool Tool { get; set; }
    }
}