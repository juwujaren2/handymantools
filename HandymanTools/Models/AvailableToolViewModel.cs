using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HandymanTools.Models
{
    public class AvailableToolViewModel
    {
        [Display(Name = "Tool ID")]
        public int ToolID;

        [Display(Name = "Abbreviated Description")]
        public string AbbreviatedDescription;

        [Display(Name = "Deposit ($)")]
        [DataType(DataType.Currency)]
        public decimal Deposit;

        [Display(Name = "Price/Day ($)")]
        [DataType(DataType.Currency)]
        public decimal RentalPrice;

    }
}