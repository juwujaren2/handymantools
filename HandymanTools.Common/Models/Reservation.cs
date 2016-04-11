﻿using System;
using System.Collections.Generic;

namespace HandymanTools.Common.Models
{
    public class Reservation
    {
        public Reservation()
        {
            this.ReservedTools = new List<ReservationTool>();
            this.PickupClerk = new Clerk();
            this.DropOffClerk = new Clerk();
        }
        public int ReservationNumber { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public int PickupClerkId { get; set; }
        public Clerk PickupClerk { get; set; }

        public int DropOffClerkId { get; set; }
        public Clerk DropOffClerk { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string CreditCardNumber { get; set; }
        public string CreditCardExpirationDate { get; set; }

        public List<ReservationTool> ReservedTools { get; set; }
    }
}