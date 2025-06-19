using IronCore.BusinessLogic.Interfaces;
using IronCore.Domain.Entities.User;
using IronCore.Domain.Entities.User.Registration;
using IronCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IronCore.Controllers
{
    public class RegistrationController : BaseController
    {
        private readonly ISession _session;

        public RegistrationController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _session = bl.GetSessionBl();
        }

        [HttpGet]
        public ActionResult Register()
        {
            ViewBag.ActivePage = "Register";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.Password != model.ConfirmPassword)
            {
                TempData["Message"] = "Пароли не совпадают";
                return View("Register");
            }

            var data = new UserRegistrationData
            {
                UserName = model.UserName,
                Password = model.Password,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                BirthDate = model.BirthDate,
                PhoneNumber = model.PhoneNumber
            };

            var result = _session.UserRegistration(data);
            if (result.Status == true)
            {
                var user = result.User;
                var userForSession = new UserDTO
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    BirthDate = user.BirthDate,
                    Balance = user.Balance,
                    PhoneNumber = user.PhoneNumber,
                    Role = user.Level.ToString(),
                };
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["Message"] = result.Message;
                return View("Register");
            }
        }
    }
}