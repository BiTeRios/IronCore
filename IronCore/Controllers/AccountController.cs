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
        private readonly UserContext db = new UserContext();
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

        [HttpGet]
        public ActionResult Login()
        {
            return View(new LoginViewModel());
        }
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel m, string returnUrl)
        {
            if (!ModelState.IsValid)
                return View(m);

            // Ищем пользователя по UserName или Email
            var user = db.Users
                .FirstOrDefault(u =>
                    u.UserName.Equals(m.Credential, StringComparison.OrdinalIgnoreCase)
                    || u.Email.Equals(m.Credential, StringComparison.OrdinalIgnoreCase)
                );

            if (user == null)
            {
                ModelState.AddModelError("", "Пользователь не найден");
                return View(m);
            }

            if (!VerifyHash(m.Password, user.Password))
            {
                ModelState.AddModelError("", "Неверный пароль");
                return View(m);
            }

            var roleString = user.Level == URole.Admin ? "Admin" : "User";
            var ticket = new FormsAuthenticationTicket(
                1, user.UserName, DateTime.Now, DateTime.Now.AddHours(4),
                false, roleString, FormsAuthentication.FormsCookiePath);
            Response.Cookies.Add(new HttpCookie(
                FormsAuthentication.FormsCookieName,
                FormsAuthentication.Encrypt(ticket)));

            user.LastLogin = DateTime.Now;
            db.SaveChanges();
            if (user.Level == URole.Admin)          
            {
                return RedirectToAction(
                    "Index",                       
                    "Users",                       
                    new { area = "Admin" });       
            }
            if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/"))
                return Redirect(returnUrl);
            return RedirectToAction("Index", "Home");
        }
        private static bool VerifyHash(string inputPassword, string storedHash)
        {
            // Хешируем введённый пароль тем же алгоритмом
            using (var sha = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(inputPassword);
                var computedHash = Convert.ToBase64String(sha.ComputeHash(bytes));
                // Сравниваем строки
                return String.Equals(computedHash, storedHash, StringComparison.Ordinal);
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();        
            return RedirectToAction("Index", "Home"); 
        }

        // GET /Account/Register
        public ActionResult Register()
        {
            ViewBag.ActivePage = "Registration";
            return View();
        }

        // POST /Account/Register
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel m)
        {
            if (!ModelState.IsValid) return View(m);

            if (db.Users.Any(u => u.Email == m.Email))
            {
                ModelState.AddModelError("", "Такой e‑mail уже зарегистрирован");
                return View(m);
            }

            var hash = Hash(m.Password);

            var user = new UserDbModel
            {
                FirstName = m.FirstName,
                LastName = m.LastName,
                UserName = m.UserName,
                PhoneNumber = m.PhoneNumber, 
                Email = m.Email,
                Password = hash,          
                LastLogin = DateTime.Now,
                BirthDate = m.BirthDate,
                Level = URole.User,    
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