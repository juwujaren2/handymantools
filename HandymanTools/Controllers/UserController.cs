using HandymanTools.Infrastructure.Repositories;
using HandymanTools.Models;
using HandymanTools.Common.Models;
using System;
using System.Web.Mvc;
using HandymanTools.Common.Enums;

namespace HandymanTools.Controllers
{
    using HandymanTools.Security;
    using System.Web.Security;

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
            User user = User.Identity.GetUser();
            if (user == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                Customer customer = user as Customer;
                if (customer != null)
                {
                    return RedirectToAction("ViewProfile", "Customer");
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
            string passwdHash = "";

            // grab password that was stored into the database.
            var password = _userRepo.GetPasswordByUserName(vm.Email, out passwdHash);
            User user = new User();

            if (vm.UserType == UserType.Customer)
            {               
                if (String.IsNullOrEmpty(password))
                {
                    return RedirectToAction("CreateProfile", "Customer");
                }
                else
                {
                    // TODO: need to refactor do passwords match to remove duplicate code in the clerk section

                    // compute the hashed password for the password inserted by the user on the login screen
                    // using the salt for the given user.
                    var hashedpassword = vm.Password.ToSHA256(passwdHash);
                    if (hashedpassword == password)
                    {
                        user = _userRepo.GetUserByUserName(vm.Email);
                        user.UserType = UserType.Customer;
                        FormsAuthentication.SetAuthCookie(user.UserName, false);
                        return RedirectToAction("ViewProfile", "Customer", user);
                    }
                    else
                    {
                        // TODO: Need to figure out how to deal with errors here. 
                        return RedirectToAction("Login");
                    }
                }
            }
            else
            {
                // TODO: need to refactor do passwords match to remove duplicate code in the clerk section

                // compute the hashed password for the password inserted by the user on the login screen
                // using the salt for the given user.
                var hashedpassword = vm.Password.ToSHA256(passwdHash);
                if (hashedpassword == password)
                {
                    user = _userRepo.GetUserByUserName(vm.UserName);
                    user.UserType = UserType.Clerk;
                    FormsAuthentication.SetAuthCookie(user.UserName, false);
                    return RedirectToAction("MainMenu", "Clerk");
                }
                return View();
            }
        }
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index");
        }
    }
}