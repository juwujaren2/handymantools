using HandymanTools.Common.Enums;
using System.Web.Mvc;

namespace HandymanTools.Controllers
{
    public class ToolController : Controller
    {
        // GET: Tool
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddTool()
        {
            //ViewBag.ToolTypes = new SelectList(ToolType, ToolType.Construction, )
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