using IronCore.BusinessLogic.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IronCore.Controllers
{
    public class CoachController : Controller
    {
        public ActionResult Coach()
        {
            return View();
        }
        public ActionResult Trainers()
        {
            ViewBag.ActivePage = "Trainers";
            return View();
        }
    }
}