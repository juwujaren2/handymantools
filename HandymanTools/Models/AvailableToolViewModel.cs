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
        public int ToolID { get; set; }

        [Display(Name = "Abbreviated Description")]
        public string AbbreviatedDescription { get; set; }

        [Display(Name = "Deposit ($)")]
        [DataType(DataType.Currency)]
        public decimal Deposit { get; set; }

        [Display(Name = "Price/Day ($)")]
        [DataType(DataType.Currency)]
        public decimal RentalPrice { get; set; }

    }
}