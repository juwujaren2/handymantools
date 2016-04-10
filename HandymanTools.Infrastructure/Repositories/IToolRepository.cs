using HandymanTools.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandymanTools.Common.Enums;

namespace HandymanTools.Infrastructure.Repositories
{
    public interface IToolRepository
    {
        List<Tool> CheckToolAvailability(ToolType toolType, DateTime startDate, DateTime endDate);

        Tool GetToolInfo(int toolId);

        List<string> GetPowerToolAccessories(Tool tool);

        int AddTool(Tool tool);

        int UpdateToolToSoldTool(int toolId);

        SaleTool GetSalesPriceForSoldTool(int toolId);
    }
}
