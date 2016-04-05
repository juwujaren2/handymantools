using HandymanTools.Common.Enums;
using HandymanTools.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HandymanTools.Models
{
    public class ToolCreateViewModel
    {
        public ToolCreateViewModel()
        {
            List<PowerToolAccessory> Accessories = new List<PowerToolAccessory>();
        }
        [Required]
        [Display(Name = "Abbreviated Description")]
        [DataType(DataType.Text)]
        public string AbbreviatedDescription { get; set; }

        [Required]
        [Display(Name = "Full Description")]
        [DataType(DataType.MultilineText)]
        public string FullDescription { get; set; }
        

        public decimal PurchasePrice { get; set; }

        [Required]
        [Display(Name = "Rental Price")]
        [DataType(DataType.Currency)]
        public decimal RentalPrice { get; set; }

        [Required]
        [Display(Name = "Deposit Amount")]
        [DataType(DataType.Currency)]
        public decimal DepositAmount { get; set; }
        
        public ToolType ToolType { get; set; }

        public List<PowerToolAccessory> Accessories { get; set; }
    }
}