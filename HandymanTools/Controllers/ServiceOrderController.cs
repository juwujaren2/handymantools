using HandymanTools.Common.Models;
using HandymanTools.Infrastructure.Repositories;
using HandymanTools.Models;
using System;
using System.Web.Mvc;

namespace HandymanTools.Controllers
{
    public class ServiceOrderController : Controller
    {
        private IServiceOrderRepository serviceOrderRepository;
        private IToolRepository toolRepository;

        public ServiceOrderController()
        {
            serviceOrderRepository = new ServiceOrderRepository();
            toolRepository = new ToolRepository();
        }
        // GET: ServiceOrder
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            ServiceOrderCreateViewModel vm = new ServiceOrderCreateViewModel();
            return View(vm);
        }

        [HttpPost]
        public ActionResult Create(ServiceOrderCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                int toolId = Convert.ToInt32(vm.ToolId);
                bool serviceOrderCreated = serviceOrderRepository.InsertServiceOrder(toolId, vm.StartingDate, vm.EndingDate, vm.EstimatedCostOfRepair);

                if (!serviceOrderCreated)
                {
                    ModelState.AddModelError("ToolId", "The tool Id entered is either invalid or currently reserved. Please enter a valid tool Id.");
                }
                else
                {
                    vm.ToolName = toolRepository.GetToolInfo(toolId).AbbrDescription;                   
                    return RedirectToAction("CreateConfirmation", vm);
                }
            }
            return View(vm);
        }

        public ActionResult CreateConfirmation(ServiceOrderCreateViewModel vm)
        {
            return View(vm);
        }
    }
}