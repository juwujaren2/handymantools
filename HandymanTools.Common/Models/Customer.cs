using System;
using System.Web;

namespace HandymanTools.Common.Models
{
    public class Customer: User
    {
        public string Address { get; set; }

        public string HomeAreaCode { get; set; }

        public string HomePhone { get; set; }

        public string WorkAreaCode { get; set; }

        public string WorkPhone { get; set; }
    }
}