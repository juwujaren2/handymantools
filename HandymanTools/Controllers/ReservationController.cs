using System;
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
        private IToolRepository toolRepository;

        public ReservationController()
        {
            reservationRepository = new ReservationRepository();
            toolRepository = new ToolRepository();
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
            ViewBag.resNumber = resNumber.ToString("D7");
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

        /// <summary>
        /// Get method for pickup reservation that returns search form
        /// </summary>
        /// <returns></returns>
        public ActionResult PickUp()
        {
            ReservationViewModel vm = new ReservationViewModel();
            vm.IsPickup = true;
            return View("Search", vm);
        }

        /// <summary>
        /// Post method for pickup reservation that returns details view
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
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
                    totalDeposit += item.Tool.DepositAmount;
                    totalCost += item.Tool.RentalPrice;
                }
                vm.EstimatedCost = totalCost;
                vm.DepositRequired = totalDeposit;
            }
            return View("Details", vm);
        }

        /// <summary>
        /// Get method for Dropoff reservation that returns the Search view
        /// </summary>
        /// <returns></returns>
        public ActionResult Dropoff()
        {
            ReservationViewModel vm = new ReservationViewModel();
            vm.IsPickup = false;
            return View("Search", vm);
        }

        /// <summary>
        /// Dropoff reservation action that returns that returns the Details view
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Dropoff(ReservationViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.IsPickup = false;
                int reservationNumber = Convert.ToInt32(vm.ReservationNumber);

                Reservation reservation = reservationRepository.GetReservationDetails(reservationNumber);
                vm.ReservedTools = reservationRepository.GetReservedToolDetails(reservationNumber);
                vm.CreditCardNumber = reservation.CreditCardNumber;
                vm.ExpirationDate = reservation.CreditCardExpirationDate;

                if (vm.ReservedTools.Count == 0)
                {
                    ModelState.AddModelError("ReservationNumber", "No reservation with that number exists. Please enter a valid number.");
                    return View("Search", vm);
                }
                decimal totalDeposit = 0;
                decimal totalCost = 0;
                foreach (var item in vm.ReservedTools)
                {
                    totalDeposit += item.Tool.DepositAmount;
                    totalCost += item.Tool.RentalPrice;
                }
                vm.EstimatedCost = totalCost;
                vm.DepositRequired = totalDeposit;
            }
            return View("Details", vm);
        }

        /// <summary>
        /// Search for both pickup and dropoff reservations
        /// </summary>
        /// <returns></returns>
        public ActionResult Search()
        {
            return PartialView();
        }

        /// <summary>
        /// Details for both pickup and dropoff reservations
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Details(ReservationViewModel vm)
        {
            if (ModelState.IsValid) {

                int reservationNumber = Convert.ToInt32(vm.ReservationNumber);

                if (vm.IsPickup)
                {
                    //set pickup clerk
                    Clerk pickupClerk = User.Identity.GetUser() as Clerk;

                    //update credit card and pickup clerk
                    reservationRepository.UpdateReservationWithCreditCard(reservationNumber, vm.CreditCardNumber, vm.ExpirationDate.Date, pickupClerk.UserName);

                    //get reservation and reservation tools information
                    Reservation reservation = reservationRepository.GetReservationDetails(reservationNumber);
                    reservation.ReservedTools = reservationRepository.GetReservedToolDetails(reservationNumber);

                    //project reservation to pass correct model type to Rental Contract view
                    RentalContractViewModel rc = new RentalContractViewModel
                    {
                        ClerkOnDuty = reservation.PickupClerk.FirstName,
                        CreditCardNumber = reservation.CreditCardNumber,
                        CustomerName = reservation.Customer.FullName,
                        DepositHeld = reservation.ReservedTools.Sum(x => x.Tool.DepositAmount),
                        EstimatedRental = reservation.ReservedTools.Sum(x => x.Tool.RentalPrice),
                        EndDate = reservation.EndDate,
                        ReservationNumber = reservation.ReservationNumber,
                        ReservedTools = reservation.ReservedTools,
                        StartDate = reservation.StartDate
                    };

                    return View("RentalContract", rc);
                }
                else
                {
                    //set pickup clerk
                    Clerk dropoffClerk = User.Identity.GetUser() as Clerk;

                    //update dropoff clerk
                    reservationRepository.UpdateReservationWithDropoffClerk(reservationNumber, dropoffClerk.UserName);

                    //get reservation and reservation tools information
                    Reservation reservation = reservationRepository.GetReservationDetails(reservationNumber);
                    reservation.ReservedTools = reservationRepository.GetReservedToolDetails(reservationNumber);

                    //project reservation to pass correct model type to Tools Receipt view
                    ToolsReceiptViewModel tr = new ToolsReceiptViewModel
                    {
                        ClerkOnDuty = reservation.DropOffClerk.FirstName,
                        CreditCardNumber = reservation.CreditCardNumber,
                        CustomerName = reservation.Customer.FullName,
                        DepositHeld = reservation.ReservedTools.Sum(x => x.Tool.DepositAmount),
                        RentalPrice = reservation.ReservedTools.Sum(x => x.Tool.RentalPrice),
                        EndDate = reservation.EndDate,
                        ReservationNumber = reservation.ReservationNumber,
                        ReservedTools = reservation.ReservedTools,
                        StartDate = reservation.StartDate
                    };
                    tr.Total = tr.RentalPrice - tr.DepositHeld;

                    //return rental receipt
                    return View("RentalReceipt", tr);
                }
            }
            return View(vm);
        }
        public ActionResult ReservedToolsDetails(int toolId)
        {
            //get tool details
            Tool tool = toolRepository.GetToolInfo(toolId);

            return View(tool);
        }
    }
}