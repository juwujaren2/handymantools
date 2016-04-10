﻿using HandymanTools.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandymanTools.Infrastructure.Repositories
{
    public interface IReservationRepository
    {
        List<Tool> GetReservedToolDetails(int reservationNumber);
        List<ReservationTool> GetReservationsByCustomer(string userName);
    }
}
