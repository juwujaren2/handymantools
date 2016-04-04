using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HandymanTools.Common.Enums;

namespace HandymanTools.Controllers
{
    public class ToolController : Controller
    {
        // GET: Tool
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Availability()
        {
            if (HttpContext.Request.RequestType != "POST") return View();
            //do work here
            var chosenToolType = Enum.Parse(typeof (ToolType), HttpContext.Request.Form["toolType"]);
            var startDate = DateTime.Parse(HttpContext.Request.Form["startDate"]);
            var endDate = DateTime.Parse(HttpContext.Request.Form["endDate"]);
            ViewBag.toolType = chosenToolType.ToString();
            ViewBag.startDate = startDate.ToShortDateString();
            ViewBag.endDate = endDate.ToShortDateString();
            return View("AvailabilityDetail");
        }
     
    }
}