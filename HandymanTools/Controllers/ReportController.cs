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
            var clerkProgressItems = reportRepo.GenerateClerkProgressReport();
            var customerRentalSummaries = reportRepo.GenerateCustomerRentalSummary();
            ReportViewModel viewModel = new ReportViewModel {
                Inventory = inventoryItems,
                ClerkProgress = clerkProgressItems,
                CustomerRental = customerRentalSummaries
            };

            return View(viewModel);
        }
    }
}