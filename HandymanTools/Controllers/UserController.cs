using HandymanTools.Infrastructure.Repositories;
using HandymanTools.Models;
using HandymanTools.Common.Models;
using System;
using System.Web.Mvc;
using HandymanTools.Common.Enums;

namespace HandymanTools.Controllers
{
    using HandymanTools.Security;
    using System.Text.RegularExpressions;
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
                    return RedirectToAction("Pickup", "Reservation");
                }
            }
        }
        
        public ActionResult Login()
        {
            LoginViewModel vm = new LoginViewModel();
            vm.UserType = UserType.Customer;

            return View(vm);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel vm)
        {
            if (ModelState.IsValid)
            {
                string passwdHash = "";
                string password = "";
                User user = new User();

                if (vm.UserType == UserType.Customer)
                {
                    //set regex for email validation
                    Regex customerEmailRegex = new Regex(@"^([0-9a-zA-Z]+[-._+&])*[0-9a-zA-Z]+@([-0-9a-zA-Z]+[.])+[a-zA-Z]{2,6}$", RegexOptions.IgnoreCase);
                    if (!customerEmailRegex.IsMatch(vm.UserName))
                    {
                        ModelState.AddModelError("Username", "Please enter a valid email address for customer username. It must be in the format: username@domain.com");
                    }
                    else
                    {
                        password = _userRepo.GetPasswordByUserName(vm.UserName, out passwdHash);
                        if (String.IsNullOrEmpty(password))
                        {
                            Session.Add("newUser", vm.UserName);
                            Session.Add("newPassword", vm.Password);
                            return RedirectToAction("CreateProfile", "Customer");
                        }
                        else
                        {
                            // compute the hashed password for the password inserted by the user on the login screen
                            // using the salt for the given user.

                            password = _userRepo.GetPasswordByUserName(vm.UserName, out passwdHash);
                            var hashedpassword = vm.Password.ToSHA256(passwdHash);
                            if (hashedpassword == password)
                            {
                                user = _userRepo.GetUserByUserName(vm.UserName);
                                user.UserType = UserType.Customer;
                                FormsAuthentication.SetAuthCookie(user.UserName, false);
                                return RedirectToAction("ViewProfile", "Customer", user);
                            }
                            else
                            {
                                return RedirectToAction("Login");
                            }
                        }
                    }
                }
                else
                {
                    //set regex for alphanumeric validation
                    Regex clerkUsernameRegex = new Regex(@"^[a-zA-Z0-9]+$", RegexOptions.IgnoreCase);
                    if (vm.UserType == UserType.Clerk && !clerkUsernameRegex.IsMatch(vm.UserName))
                    {
                        ModelState.AddModelError("Username", "Only alphanumeric characters allowed for clerk username. Please use letters and numbers only.");
                    }
                    else
                    {
                        // compute the hashed password for the password inserted by the user on the login screen
                        // using the salt for the given user.
                        password = _userRepo.GetPasswordByUserName(vm.UserName, out passwdHash);
                        var hashedpassword = vm.Password.ToSHA256(passwdHash);
                        if (hashedpassword == password)
                        {
                            //user = _userRepo.GetUserByUserName(vm.UserName);
                            user.UserType = UserType.Clerk;
                            FormsAuthentication.SetAuthCookie(vm.UserName, false);
                            return RedirectToAction("Pickup", "Reservation");
                        }
                        return View();
                    }
                }
            }
            return View();
        }
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}