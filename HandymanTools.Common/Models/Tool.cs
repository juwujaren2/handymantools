using HandymanTools.Common.Enums;
using System;

namespace HandymanTools.Common.Models
{
    public class Tool
    {
        public int ToolId { get; set; }
        public string AbbrDescription { get; set; }
        public string FullDescription { get; set; }
        public decimal RentalPrice { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal DepositAmount { get; set; }
        public ToolType ToolType { get; set; }
        public DateTime? SaleDate { get; set; }
    }
}