using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IronCore.Controllers
{
    public class TrainersController : Controller
    {
        // GET: Trainers
        public ActionResult Trainers()
        {
            ViewBag.ActivePage = "Trainers";
            return View();
        }
        public ActionResult Coach()
        {
            return View();
        }
    }
}