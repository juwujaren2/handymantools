using HandymanTools.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HandymanTools.Models
{
    public class ReportViewModel
    {
        public List<InventoryItem> Inventory { get; set; }

        public List<ClerkProgressItem> ClerkProgress { get; set; }
        
        public List<CustomerRentalSummary> CustomerRental { get; set; }
    }
}