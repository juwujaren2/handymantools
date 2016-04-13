using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandymanTools.Common.Models
{
    public class ClerkProgressItem
    {
        public Clerk Clerk { get; set; }

        public int Pickups { get; set; }

        public int Dropoffs { get; set; }

        public int TotalPickupsDropoffs { get; set; }
    }
}
