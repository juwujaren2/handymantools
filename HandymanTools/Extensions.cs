using HandymanTools.Common.Models;
using HandymanTools.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace HandymanTools
{
    public static class Extensions
    {
        public static User GetUser(this IIdentity identity)
        {
            UserRepository userRepo = new UserRepository();
            User user = identity.IsAuthenticated ? userRepo.GetUserByUserName(identity.Name) : null;
            
            return user;
        }
    }
}