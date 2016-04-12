using HandymanTools.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HandymanTools.Models
{
    public class RentalContractViewModel
    {
        public int ReservationNumber { get; set; }

        public string CustomerName { get; set; }

        public string ClerkOnDuty { get; set; }

        [DataType(DataType.CreditCard)]
        public string CreditCardNumber { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public List<ReservationTool> ReservedTools { get; set; }

        [DataType(DataType.Currency)]
        public decimal DepositHeld { get; set; }

        [DataType(DataType.Currency)]
        public decimal EstimatedRental { get; set; }
    }
}