using FraudTransactions.DataModel;
using FraudTransactions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FraudTransactions.Controllers
{
    /// <summary>
    /// Controller for Account management
    /// </summary>
    public class AccountController : Controller
    {
        static IAccountRepository _repository;
        public AccountController()
        {
            _repository = new AccountRepository();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(RegisterViewModel model)
        {
            FraudTransactions.Models.User user = new Models.User() {
                Password=model.Password,
                UserName=model.UserName,
                RoleId= model.Role
            };
            var existingUser=_repository.Get(model.UserName);
            if (existingUser != null)
            {
                ModelState.AddModelError("", "The user name already exists");
                return View(model);
            }
            else
            {
               _repository.Add(user);
               
            }
            return View("Login");

        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FraudTransactions.Models.RegisterViewModel user,string returnUrl)
        {
            if (ModelState.IsValid)
            {
                bool userValid = _repository.IsValidUser(user.UserName, user.Password);
                if (userValid)
                {

                    FormsAuthentication.SetAuthCookie(user.UserName, false);
                    System.Web.HttpContext.Current.Session["username"] = user.UserName;
                    System.Web.HttpContext.Current.Session["password"] = user.Password;
                    //HttpContext.Current.Session["token"];
                    return RedirectToAction("Index", "Transactions");
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }
            return View(user);
        }
        [HttpPost]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Main");
        }

    }
}