using System;
using HandymanTools.Common.Enums;
using HandymanTools.Infrastructure.Repositories;
using System.Web.Mvc;

namespace HandymanTools.Controllers
{
    public class ToolController : Controller
    {
        // GET: Tool
        public ActionResult Detail(int toolId)
        {
            var tool = new ToolRepository().GetToolInfo(toolId);
            var toolView = new Models.ToolViewModel()
            {
                ToolId = tool.ToolId,
                AbbreviatedDescription = tool.AbbrDescription,
                Accessories = tool.Accessories,
                DepositAmount = tool.DepositAmount,
                FullDescription = tool.FullDescription,
                PurchasePrice = tool.PurchasePrice,
                RentalPrice = tool.RentalPrice,
                ToolType = tool.ToolType
            };

            return View(toolView);
        }

        public ActionResult Add()
        {
            //ViewBag.ToolTypes = new SelectList(ToolType, ToolType.Construction, )
            return View();
        }

        public ActionResult Availability()
        {
            if (HttpContext.Request.RequestType != "POST") return View();
            //do work here
            var chosenToolType = (ToolType)Enum.Parse(typeof (ToolType), HttpContext.Request.Form["toolType"]);
            var startDate = DateTime.Parse(HttpContext.Request.Form["startDate"]);
            var endDate = DateTime.Parse(HttpContext.Request.Form["endDate"]);
            ViewBag.toolType = chosenToolType.ToString();
            ViewBag.startDate = startDate.ToShortDateString();
            ViewBag.endDate = endDate.ToShortDateString();
            var viewModelList = new Models.AvailableToolsViewModel();            
            foreach (var tool in new ToolRepository().CheckToolAvailability(chosenToolType, startDate, endDate))
            {
                viewModelList.Add(new Models.AvailableToolViewModel()
                {
                    ToolID =  tool.ToolId,
                    AbbreviatedDescription = tool.AbbrDescription,
                    Deposit = tool.DepositAmount,
                    RentalPrice = tool.RentalPrice
                });
            }
            
            ViewData.Add("AvailableTools", viewModelList);

            return View("AvailabilityDetail");
        }
     
    }
}