using HandymanTools.Common.Models;
using HandymanTools.Infrastructure.Repositories;
using HandymanTools.Models;
using HandymanTools.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HandymanTools.Controllers
{
    public class CustomerController : Controller
    {
        private ICustomerRepository m_customerRepository;

        public CustomerController()
        {
            m_customerRepository = new CustomerRepository();
        }

        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateProfile()
        {
            return View();
        }

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


            }
            // TODO: Need to add an Error View if this fails with the appropriate message.
            
            return View();
            
        }

        public ActionResult ViewProfile()
        {
            Customer customer = User.Identity.GetUser() as Customer;

            return View(customer);
        }
    }
}