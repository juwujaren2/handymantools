using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandymanTools.Common.Enums;
using HandymanTools.Common.Models;

namespace HandymanTools.Infrastructure.Repositories
{
    class ToolRepository
    {
        public List<Tool> CheckToolAvailability(ToolType toolType, DateTime startDate, DateTime endDate)
        {
            return null;
        }

        public Tool GetToolInfo(int toolId)
        {
            return null;
        }

        public bool SellTool(int toolId)
        {
            return true;
        }

        public int AddTool(Tool tool)
        {
            return 0;
        }
    }
}
