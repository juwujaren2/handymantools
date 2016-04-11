using System.Web.Mvc;
using HandymanTools.Infrastructure.Repositories;
using HandymanTools.Models;

namespace HandymanTools.Controllers
{
    public class ReportController : Controller
    {
        private IReportRepository reportRepo;

        public ReportController()
        {
            reportRepo = new ReportRepository();
        }

        // GET: Report
        public ActionResult Index()
        {
            var inventoryItems = reportRepo.GenerateInventoryReport();

            ReportViewModel viewModel = new ReportViewModel {
                Inventory = inventoryItems
            };

            return View(viewModel);
        }
    }
}