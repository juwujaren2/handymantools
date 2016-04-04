using HandymanTools.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandymanTools.Common.Models
{
    public class UserIdentity
    {
        public string UserName { get; set; }
        public virtual UserType UserType { get; set; }
    }
}
