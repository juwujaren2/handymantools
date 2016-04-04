using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HandymanTools.Common.Enums;

namespace HandymanTools.Common.Models
{
    public class Clerk : User
    {
        public override UserType UserType
        {
            get
            {
                return UserType.Clerk;
            }
        }
    }
}