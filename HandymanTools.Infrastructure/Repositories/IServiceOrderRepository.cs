﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandymanTools.Infrastructure.Repositories
{
    public interface IServiceOrderRepository
    {
        bool InsertServiceOrder(int toolId, DateTime startDate, DateTime endDate, decimal estimatedCost);
    }
}
