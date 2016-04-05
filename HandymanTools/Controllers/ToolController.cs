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
    }
}