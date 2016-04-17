using System;
using System.Collections.Generic;

namespace HandymanTools.Models
{
    public class ProfileReservationsViewModel
    {
        public string Tools { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartDate { get; set; }
        public string PickupClerk { get; set; }
        public string DropoffClerk { get; set; }
        public decimal TotalRental { get; set; }
        public decimal TotalDeposit { get; set; }
    }
}