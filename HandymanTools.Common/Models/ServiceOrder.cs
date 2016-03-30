using System;
using System.Web;

namespace HandymanTools.Common.Models
{
    public class ServiceOrder
    {
        public int ToolId { get; set; }
        public Tool Tool { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public decimal EstimatedCost { get; set; }
    }
}