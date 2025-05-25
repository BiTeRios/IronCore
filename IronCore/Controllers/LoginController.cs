using IronCore.BusinessLogic.Interfaces;
using System;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using IronCore.Domain.Entities.User.Login;
using IronCore.Models;
using IronCore.Domain.Entities.User;

namespace IronCore.Controllers
{
    public class LoginController : Controller
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
            if (ModelState.IsValid)
            {
                UserLoginData data = new UserLoginData
                {
                    UserName = login.UserName,
                    Password = login.Password,
                    LoginDateTime = DateTime.Now,
                    LoginIp = Request.UserHostAddress
                };
                var userLoginResult = _session.UserLogin(data);
                if (userLoginResult.Status == true)
                {
                    HttpCookie cookie = _session.GenCookie(login.UserName);
                    ControllerContext.HttpContext.Response.Cookies.Add(cookie);


                    StoreUserInSession(userLoginResult.UserDTO);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["Message"] = userLoginResult.Message;
                    return View("Login");
                }
            }
            TempData["Message"] = "Something went wrong";
            return View("Login");
        }
        public void StoreUserInSession(UserDTO user)
        {
            SessionHelper.User = user;
        }

    }
}