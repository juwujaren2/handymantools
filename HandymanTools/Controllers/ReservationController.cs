﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.ModelBinding;
using System.Web.Mvc;
using HandymanTools.Infrastructure.Repositories;
using HandymanTools.Models;
using HandymanTools.Common.Models;

namespace HandymanTools.Controllers
{
    public class ReservationController : Controller
    {
        private IReservationRepository reservationRepository;

        public ReservationController()
        {
            reservationRepository = new ReservationRepository();
        }
        // GET: Reservation
        public ActionResult Make()
        {
            return View();
        }

        public ActionResult Confirmation(FormCollection formData)
        {
            var startDate = DateTime.Parse(formData["startDate"]);
            var endDate = DateTime.Parse(formData["endDate"]);
            ViewBag.StartDate = startDate.ToShortDateString();
            ViewBag.EndDate = endDate.ToShortDateString();
            var totalRental = decimal.Parse(formData["totalRental"]);
            ViewBag.TotalRental = totalRental;
            var totalDeposit = decimal.Parse(formData["totalDeposit"]);
            ViewBag.TotalDeposit = totalDeposit;
            var customerId = User.Identity.GetUser().UserName;
            var tools = new Dictionary<int, string>();
            for (var i = 0; i < formData.AllKeys.Length; i++)
            {
                if (formData.AllKeys[i].Contains("toolKey"))
                {
                    tools.Add(int.Parse(formData[i]), formData["toolName"+ formData[i].Replace("toolKey", "")]);
                }
            }
            var toolIds = tools.Keys.ToList();
            var toolNames = tools.Values.ToList();
            var reservationRespository = new ReservationRepository();
            var resNumber = reservationRespository.MakeReservation(customerId, startDate, endDate, toolIds);
            ViewBag.resNumber = resNumber;
            return View(toolNames);
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
            var toolRepository = new ToolRepository();
            for (var i = 0; i < formData.AllKeys.Length; i++)
            {
                if (!formData.AllKeys[i].Contains("toolSelection")) continue;
                var tool = toolRepository.GetToolInfo(int.Parse(formData[i]));
                tools.Add(tool.ToolId, tool.AbbrDescription);
                totalRental += tool.RentalPrice;
                totalDeposit += tool.DepositAmount;
            }
            ViewBag.TotalRental = totalRental;
            ViewBag.totalDeposit = totalDeposit;
            return View(tools);
        }
        public ActionResult PickUp()
        {
            ReservationViewModel vm = new ReservationViewModel();
            vm.IsPickup = true;
            return View("Search", vm);
        }

        [HttpPost]
        public ActionResult Pickup(ReservationViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.IsPickup = true;
                int reservationNumber = Convert.ToInt32(vm.ReservationNumber);
                vm.ReservedTools = reservationRepository.GetReservedToolDetails(reservationNumber);

                if (vm.ReservedTools.Count == 0)
                {
                    ModelState.AddModelError("ReservationNumber", "No reservation with that number exists. Please enter a valid number.");
                    return View("Search", vm);
                }
                decimal totalDeposit = 0;
                decimal totalCost = 0;
                foreach (var item in vm.ReservedTools)
                {
                    totalDeposit += item.DepositAmount;
                    totalCost += item.RentalPrice;
                }
                vm.EstimatedCost = totalCost;
                vm.DepositRequired = totalDeposit;
            }
            return View("Details", vm);
        }

        public ActionResult Dropoff()
        {
            ReservationViewModel vm = new ReservationViewModel();
            vm.IsPickup = false;
            return View("Search", vm);
        }

        [HttpPost]
        public ActionResult Dropoff(ReservationViewModel vm)
        {
            vm.IsPickup = false;
            int reservationNumber = Convert.ToInt32(vm.ReservationNumber);
            vm.ReservedTools = reservationRepository.GetReservedToolDetails(reservationNumber);

            decimal totalDeposit = 0;
            decimal totalCost = 0;
            foreach (var item in vm.ReservedTools)
            {
                totalDeposit += item.DepositAmount;
                totalCost += item.RentalPrice;
            }

            return View("Details", vm);
        }
        public ActionResult Search()
        {
            return PartialView();
        }
    }
}