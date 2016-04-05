using HandymanTools.Common.Enums;
using System;
using System.Collections.Generic;

namespace HandymanTools.Common.Models
{
    public class Tool
    {
        public Tool()
        {
            List<PowerToolAccessory> Accessories = new List<PowerToolAccessory>();
        }
        public int ToolId { get; set; }

        public string AbbrDescription { get; set; }

        public string FullDescription { get; set; }

        public decimal RentalPrice { get; set; }

        public decimal PurchasePrice { get; set; }

        public decimal DepositAmount { get; set; }

        public ToolType ToolType { get; set; }

        public DateTime? SaleDate { get; set; }

        public List<PowerToolAccessory> Accessories { get; set; }
    }
}