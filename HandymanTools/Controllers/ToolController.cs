using System;
using HandymanTools.Common.Enums;
using HandymanTools.Common.Models;
using HandymanTools.Infrastructure.Repositories;
using HandymanTools.Models;
using System.Linq;
using System.Web.Mvc;

namespace HandymanTools.Controllers
{
    public class ToolController : Controller
    {
        private IToolRepository toolRepository;

        public ToolController()
        {
            toolRepository = new ToolRepository();
        }
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
            ToolCreateViewModel vm = new ToolCreateViewModel();         
            return View(vm);
        }

        [HttpPost]
        public ActionResult Add(ToolCreateViewModel vm)
        {
            if (ModelState.IsValid)
            { 
                if (vm.ToolType != ToolType.Power)
                {
                    vm.Accessories.Clear();
                }
                Tool tool = new Tool();
                tool.AbbrDescription = vm.AbbreviatedDescription;
                tool.Accessories = vm.Accessories.Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
                tool.DepositAmount = vm.DepositAmount;
                tool.FullDescription = vm.FullDescription;
                tool.PurchasePrice = vm.PurchasePrice;
                tool.RentalPrice = vm.RentalPrice;
                tool.ToolType = vm.ToolType;

                var toolId = toolRepository.AddTool(tool);

                return RedirectToAction("PickUp", "Reservation");
            }
            return View(vm);
        }

        public ActionResult Sell()
        {
            ToolSellViewModel vm = new ToolSellViewModel();
            return View(vm);
        }
        [HttpPost]
        public ActionResult Sell(ToolSellViewModel vm)
         {            
            if (ModelState.IsValid)
            {
                int toolId = Convert.ToInt32(vm.ToolId);
                //Update tool to tool that's now available for sale
                toolRepository.UpdateToolToSoldTool(toolId);

                //Calculate sales price for tool for sale
                SaleTool saleTool = toolRepository.GetSalesPriceForSoldTool(toolId);

                if (saleTool.ToolId == 0)
                {
                    ModelState.AddModelError("ToolId", "No tool with that Id exists or is available for sale. Please enter a valid Id.");
                }

                vm.ToolId = saleTool.ToolId.ToString();
                vm.ToolName = saleTool.AbbreviatedDescription;
                vm.SalesPrice = saleTool.SalesPrice;
                vm.SalesDate = saleTool.SalesDate;            
            }
            return View(vm);
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