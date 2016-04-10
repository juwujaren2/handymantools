using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandymanTools.Common.Models
{
    public class SaleTool
    {
        public int ToolId { get; set; }

        public string AbbreviatedDescription { get; set; }

        public decimal SalesPrice { get; set; }

        public DateTime SalesDate { get; set; }
    }
}
