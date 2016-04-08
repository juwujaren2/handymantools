using System;
using System.Collections.Generic;
using System.Web.Http.ModelBinding;
using System.Web.Mvc;
using HandymanTools.Infrastructure.Repositories;

namespace HandymanTools.Controllers
{
    public class ReservationController : Controller
    {
        // GET: Reservation
        public ActionResult Make()
        {
            return View();
        }

        public ActionResult Confirmation(FormCollection formData)
        {
            return View();
        }

        public ActionResult Summary(FormCollection formData)
        {
            var startDate = DateTime.Parse(formData["startDate"]);
            var endDate = DateTime.Parse(formData["endDate"]);
            ViewBag.StartDate = startDate.ToShortDateString();
            ViewBag.EndDate = endDate.ToShortDateString();
            var tools = new Dictionary<int, string>();
            decimal totalRental = 0;
            decimal totalDeposit = 0;
            var ToolRepository = new ToolRepository();
            for (int i = 0; i < formData.AllKeys.Length; i++)
            {
                if (formData.AllKeys[i].Contains("toolSelection"))
                {
                    var tool = ToolRepository.GetToolInfo(int.Parse(formData[i]));
                    tools.Add(tool.ToolId, tool.AbbrDescription);
                    totalRental += tool.RentalPrice;
                    totalDeposit += tool.DepositAmount;
                }
            }
            ViewBag.TotalRental = totalRental;
            ViewBag.totalDeposit = totalDeposit;
            return View(tools);
        }
        public ActionResult PickUpReservation()
        {
            return View();
        }
    }
}