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
    public class LoginController : BaseController
    {
        private readonly ISession _session;
        public LoginController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _session = bl.GetSessionBl();
        }
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
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var loginData = new UserLoginData
            {
                UserName = login.UserName,
                Password = login.Password,
                LoginIp = Request.UserHostAddress
            };

            var result = _session.UserLogin(loginData);

            if (result.Status)
            {
                string roles = result.UserDTO.Role ?? "User";

                var authTicket = new FormsAuthenticationTicket(
                    1,
                    result.UserDTO.UserName,
                    DateTime.Now,
                    DateTime.Now.AddMinutes(30),
                    false,
                    roles
                );

                string encTicket = FormsAuthentication.Encrypt(authTicket);
                var faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                Response.Cookies.Add(faCookie);

                Session["UserId"] = result.UserDTO.Id;

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", result.Message ?? "Ошибка входа");
                return View(login);
            }
        }
    }
}