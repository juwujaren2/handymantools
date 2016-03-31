using HandymanTools.Infrastructure.Repositories;
using HandymanTools.Models;
using HandymanTools.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HandymanTools.Common.Enums;

namespace HandymanTools.Controllers
{
    using HandymanTools.Security;
    public class UserController : Controller
    {
        private UserRepository _userRepo;
        public UserController()
        {
            _userRepo = new UserRepository();
        }
        // GET: User
        public ActionResult Index()
        {
            User user = (User)Session["LoggedInUser"];
            if (user == null)
            {
                RedirectToAction("Login");
            }
            else
            {
                Customer customer = user as Customer;
                if (customer != null)
                {
                    //return to Customer Index
                }
                else
                {
                    //return to Clerk Index
                }
            }
            return View();
        }
        
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel vm)
        {
            var password = _userRepo.GetPasswordByUserName(vm.Email);
            // TODO: Need to write stored procedure to get password hash
            // out of the database.
            var hashedpassword = vm.Password.ToSHA256("somerandomsalt");

            if (vm.UserType == UserType.Customer)
            {
                if (String.IsNullOrEmpty(password))
                {
                    return RedirectToAction("CreateProfile", "Customer");
                }
                else
                {
                    if (hashedpassword == password)
                        return RedirectToAction("ViewProfile", "Customer");
                    else
                    {
                        // TODO: Need to figure out how to deal with errors here. 
                        return RedirectToAction("Login");
                    }
                }
            }
            //var user = vm.UserType == UserType.Customer ? _userRepo.GetPasswordByUserName(vm.Email) : _userRepo.GetPasswordByUserName(vm.UserName);
            //if customer type selected and user name not in database, return create new customer profile view
            //if customer type selected and user name is in database, return password from database and validate against password from 
            else
            {
                if (hashedpassword == password)
                    return RedirectToAction("MainMenu", "Clerk");
                return View();
            }
        }
    }
}