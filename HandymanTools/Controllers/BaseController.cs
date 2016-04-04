﻿using HandymanTools.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HandymanTools.Controllers
{
    public class BaseController : Controller
    {
        public void SetSessionUser(User user)
        {
            Session["LoggedInUser"] = user;
        }
        public User GetSessionUser()
        {
            return (User)Session["LoggedInUser"];
        }
        public bool IsAuthenticated()
        {
            return GetSessionUser() != null;
        }

    }
}