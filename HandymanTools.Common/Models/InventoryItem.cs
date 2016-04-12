using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandymanTools.Common.Models
{
    public class InventoryItem
    {
        public Tool Tool { get; set; }

        public decimal RentalProfit { get; set; }

        public decimal CostOfRepairs { get; set; }

        public decimal TotalProfit { get; set; }
    }
}
