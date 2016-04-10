using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HandymanTools.Models
{
    public class ToolSellViewModel
    {
        [RegularExpression("^[0-9]*$", ErrorMessage = "Search value must be a valid integer greater than 0")]
        [DisplayName("Tool")]
        public string ToolId { get; set; }
        
        [DisplayName("Tool Name")]
        public string ToolName { get; set; }

        [DataType(DataType.Currency)]
        [DisplayName("Sales Price")]
        public decimal SalesPrice { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Sales Date")]
        public DateTime SalesDate { get; set; }
    }
}