using HandymanTools.Common.Enums;
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
        public override UserType UserType
        {
            get
            {
                return UserType.Customer;
            }
        }

        public string FullHomePhone
        {
            get
            {
                string fullPhone = string.Format("{0}{1}", HomeAreaCode, HomePhone);
                return string.Format("{0:(###) ###-####}", fullPhone);
            }
        }

        public string FullWorkPHone
        {
            get
            {
                string fullPhone = string.Format("{0}{1}", WorkAreaCode, WorkPhone);
                return string.Format("{0:(###) ###-####}", fullPhone);
            }
        }

    }
}