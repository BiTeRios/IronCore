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
        private readonly CoachBL _bl = new CoachBL();
        public ActionResult Index() => View(_bl.getAllCoaches());

        public ActionResult Details(int id)
        {
            var coach = _bl.getInfoAboutCoach(id);
            return coach == null ? (ActionResult)HttpNotFound() : View(coach);
        }
    }
}