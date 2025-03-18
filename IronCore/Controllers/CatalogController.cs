using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IronCore.Controllers
{
    public class CatalogController : Controller
    {
        // GET: Catalog
        public ActionResult Programs()
        {
            ViewBag.ActivePage = "Programs";
            return View();
        }
        public ActionResult ProgramDetail()
        {
            return View();
        }
        public ActionResult Nutritions()
        {
            ViewBag.ActivePage = "Nutritions";
            return View();
        }
    }
}