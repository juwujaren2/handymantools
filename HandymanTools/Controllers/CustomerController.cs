using HandymanTools.Common.Models;
using HandymanTools.Infrastructure.Repositories;
using HandymanTools.Models;
using HandymanTools.Security;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;

namespace HandymanTools.Controllers
{
    public class CustomerController : Controller
    {
        private ICustomerRepository m_customerRepository;
        private IReservationRepository _reservationRepo;

        public CustomerController()
        {
            m_customerRepository = new CustomerRepository();
            _reservationRepo = new ReservationRepository();
        }

        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Loads the Create customer profile form
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateProfile()
        {
            return View();
        }

        /// <summary>
        /// Saves customer profile
        /// </summary>
        /// <param name="model">Viewmodel to pass data from the view to the controller</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateProfile(CustomerCreateProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                Customer customer = new Customer();
                customer.UserName = model.UserName;
                customer.FirstName = model.FirstName;
                customer.LastName = model.LastName;
                customer.HomeAreaCode = model.HomeAreaCode;
                customer.HomePhone = model.HomePhone;
                customer.WorkAreaCode = model.WorkAreaCode;
                customer.WorkPhone = model.WorkPhone;
                customer.Address = model.Address;

                // encrypt the password prior to insertion and grab the password hash for insertion.
                SaltGenerator generator = new SaltGenerator();
                customer.PasswordHash = generator.Salt;
                customer.Password = model.Password.ToSHA256(customer.PasswordHash);
                var customerId = m_customerRepository.AddCustomer(customer);

                FormsAuthentication.SetAuthCookie(customer.UserName, false);
                return RedirectToAction("ViewProfile", "Customer");
            }
            
            return View();
            
        }

        /// <summary>
        /// Displays profile of currently logged in user if customer
        /// </summary>
        /// <returns></returns>
        public ActionResult ViewProfile()
        {
            Customer customer = User.Identity.GetUser() as Customer;
            CustomerProfileViewModel vm = new CustomerProfileViewModel
            {
                Address = customer.Address,
                Email = customer.UserName,
                Name = customer.FullName,
                HomePhone = customer.FullHomePhone,
                WorkPhone = customer.FullWorkPHone
            };
            vm.Reservations = _reservationRepo.GetReservationsByCustomer(customer.UserName);
            return View(vm);
        }
    }
}