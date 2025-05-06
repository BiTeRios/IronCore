using IronCore.BusinessLogic.Interfaces;
using IronCore.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using IronCore.BusinessLogic.Core;
using IronCore.Models;

namespace IronCore.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Registration()
        {
            ViewBag.ActivePage = "Registration";
            return View();
        }
        public ActionResult User()
        {
            return View();
        }
        public ActionResult ForgotPass()
        {
            return View();
        }
        public ActionResult ShoppingCart()
        {
            return View();
        }

        private readonly UserApi _authService;

        public AccountController()
        {
            _authService = new UserApi();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _authService.Login(model.Credential, model.Password);
            if (user != null)
            {
                var viewModel = MvcApplication.MapperInstance.Map<LoginViewModel>(user);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid login or password");
            return View(model);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}