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
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddTool()
        {
            ToolCreateViewModel vm = new ToolCreateViewModel();         
            return View(vm);
        }

        [HttpPost]
        public ActionResult AddTool(ToolCreateViewModel vm)
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

                return RedirectToAction("PickUpReservation", "Reservation");
            }
            return View(vm);
        }

        public ActionResult AddAccessory()
        {
            return View();
        }
    }
}