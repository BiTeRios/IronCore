﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IronCore.Controllers
{
    public class HomeController : Controller
    {
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
        public ActionResult Programs()
        {
            ViewBag.ActivePage = "Programs";
            return View();
        }
        public ActionResult Nutritions()
        {
            ViewBag.ActivePage = "Nutritions";
            return View();
        }
        public ActionResult Trainers()
        {
            ViewBag.ActivePage = "Trainers";
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.ActivePage = "Contact";
            return View();
        }
        public ActionResult Login()
        {
            ViewBag.ActivePage = "Login";
            return View();
        }
        public ActionResult Page404()
        {
            return View();
        }
        public ActionResult User()
        {
            return View();
        }
        public ActionResult ProgramDetail()
        {
            return View();
        }
        public ActionResult PrivacyPolicy()
        {
            return View();
        }
        public ActionResult Coach()
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
        public ActionResult Registration()
        {
            return View();
        }
    }
}