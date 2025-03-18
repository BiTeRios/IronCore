using System;
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
        public ActionResult Contact()
        {
            ViewBag.ActivePage = "Contact";
            return View();
        }
        public ActionResult PrivacyPolicy()
        {
            return View();
        }
    }
}