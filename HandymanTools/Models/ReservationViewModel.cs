using HandymanTools.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HandymanTools.Models
{
    public class ReservationViewModel
    {
        [RegularExpression("^[0-9]*$", ErrorMessage = "Search value must be a valid integer greater than 0")]
        public string ReservationNumber { get; set; }

        public bool IsPickup { get; set; }

        [ReadOnly(true)]
        public List<Tool> ReservedTools { get; set; }

        public string CustomerId { get; set; }

        [ReadOnly(true)]
        [DataType(DataType.Currency)]
        public decimal DepositRequired { get; set; }

        [ReadOnly(true)]
        [DataType(DataType.Currency)]
        public decimal EstimatedCost { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "Search value must be a valid integer greater than 0")]
        public string ToolId { get; set; }

        [Display(Name = "Credit Card Number")]
        [DataType(DataType.CreditCard)]
        public string CreditCardNumber { get; set; }

        [Display(Name = "Expiration Date")]
        public string ExpirationDate { get; set; }
    }
}