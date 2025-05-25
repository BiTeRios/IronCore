using IronCore.BusinessLogic.Interfaces;
using System;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using IronCore.Domain.Entities.User.Login;
using IronCore.Models;
using IronCore.Domain.Entities.User;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace IronCore.Controllers
{
    public class LoginController : Controller
    {
        private readonly ISession _session;
        public LoginController(ISession session) { _session = session; }

        [HttpGet]
        public ActionResult Login()
        {
            ViewBag.ActivePage = "Login";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel login)
        {
            if (!ModelState.IsValid) return View(login);

            var res = _session.UserLogin(new UserLoginData
            {
                UserName = login.UserName,
                Password = login.Password,
                LastLogin = DateTime.UtcNow,
                LoginIp = Request.UserHostAddress
            });

            if (!res.Status)
            {
                ModelState.AddModelError("", res.Message);
                return View(login);
            }

            FormsAuthentication.SetAuthCookie(login.UserName, false);
            return RedirectToAction("Index", "Home");
        }
    }
}