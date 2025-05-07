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

        private readonly UserContext db = new UserContext();   // как и раньше

        // GET /Account/Register
        public ActionResult Register() => View();

        // POST /Account/Register
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel m)
        {
            if (!ModelState.IsValid) return View(m);

            // проверяем уникальность e‑mail
            if (db.Users.Any(u => u.Email == m.Email))
            {
                ModelState.AddModelError("", "Такой e‑mail уже зарегистрирован");
                return View(m);
            }

            // простой SHA‑256 (чтобы не добавлять новых пакетов)
            var hash = Hash(m.Password);

            var user = new UserDbModel
            {
                UserName = m.UserName,
                Email = m.Email,
                Password = hash,          // поле так и называется в Entity
                LastLogin = DateTime.Now,
                Level = URole.User,    // роль «обычный пользователь»
                RegistrationDate = DateTime.Now
            };

            db.Users.Add(user);
            db.SaveChanges();

            FormsAuthentication.SetAuthCookie(m.Email, false);
            return RedirectToAction("Index", "Home");
        }

        private static string Hash(string src)
        {
            using (var sha = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(src);
                return Convert.ToBase64String(sha.ComputeHash(bytes));
            }
        }
    }
}