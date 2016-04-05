using HandymanTools.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandymanTools.Infrastructure.Repositories
{
    public interface IToolRepository
    {
        int AddTool(Tool tool);

        int AddPowerToolAccessory(int toolId, string accessory);

        int SellTool(int ToolId);
    }
}
