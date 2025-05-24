using IronCore.BusinessLogic.Interfaces;
using IronCore.Domain.Entities.Contact;
using IronCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IronCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IContact _contact;
        public HomeController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _contact = bl.GetContactBL();
        }
        public ActionResult Index()
        {
            ViewBag.ActivePage = "Index";
            return View();
        }
        public ActionResult Why()
        {
            ViewBag.ActivePage = "Why";
            return View();
        }
        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.ActivePage = "Contact";
            return View(new ContactViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactViewModel md)
        {
            if (ModelState.IsValid)
            {
                var data = new ContactDTO
                {
                    Id = md.Id,
                    Name = md.Name,
                    Email = md.Email,
                    PhoneNumber = md.PhoneNumber,
                    Message = md.Message
                };
                if (_contact.CreateContact(data))
                {
                    TempData["Success"] = "Спасибо! Ваше сообщение отправлено.";
                    return RedirectToAction("Contact");
                }

                ModelState.AddModelError("", "Не удалось сохранить сообщение. Попробуйте ещё раз.");
            }
            return View(md);
        }
        public ActionResult PrivacyPolicy()
        {
            return View();
        }
    }
}