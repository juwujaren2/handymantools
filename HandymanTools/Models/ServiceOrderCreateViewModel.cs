using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HandymanTools.Models
{
    public class ServiceOrderCreateViewModel
    {
        [Required]
        [Display(Name = "Tool ID")]
        public string ToolId { get; set; }

        public string ToolName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Starting Date")]
        public DateTime StartingDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Ending Date")]
        public DateTime EndingDate { get; set; }
        
        [Required]
        [Range(1.0, Double.MaxValue)]
        [DataType(DataType.Currency)]
        [Display(Name = "Estimated Cost of Repair")]
        public decimal EstimatedCostOfRepair { get; set; }
    }
}