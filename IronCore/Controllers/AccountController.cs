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
using IronCore.BusinessLogic.DBModel;
using IronCore.Domain.Entities.User;
using IronCore.Domain.Enums.User;
using IronCore.Filters;

namespace IronCore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserApi _authService;
        public ActionResult ForgotPass()
        {
            return View();
        }
        public ActionResult ShoppingCart()
        {
            return View();
        }
        public AccountController()
        {
            _authService = new UserApi();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login", "Login");
        }
    }
}