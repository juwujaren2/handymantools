using HandymanTools.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HandymanTools.Models
{
    public class CustomerProfileViewModel
    {
        public string Email { get; set; }

        public string Name { get; set; }

        public string HomePhone { get; set; }

        public string WorkPhone { get; set; }

        public string Address { get; set; }

        public List<ReservationTool> Reservations { get; set; }
    }
}