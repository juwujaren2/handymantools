using HandymanTools.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HandymanTools.Common.Models
{
    public class Tool
    {
        public Tool()
        {
            Accessories = new List<string>();
        }
        public int ToolId { get; set; }

        public string AbbrDescription { get; set; }

        public string FullDescription { get; set; }

        [DataType(DataType.Currency)]
        public decimal RentalPrice { get; set; }

        [DataType(DataType.Currency)]
        public decimal PurchasePrice { get; set; }

        [DataType(DataType.Currency)]
        public decimal DepositAmount { get; set; }

        public ToolType ToolType { get; set; }

        public DateTime? SaleDate { get; set; }

        public List<string> Accessories { get; set; }
    }
}