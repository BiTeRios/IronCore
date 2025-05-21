using System.Web.Mvc;
using IronCore.Filters;          
using IronCore.BusinessLogic;

namespace IronCore.Controllers
{
    [AdminMod]
    public class AdminController : Controller
    {

        public ActionResult Index()
        {
            ViewBag.ActivePage = "Index";
            return View();                   // Views/Admin/Index.cshtml
        }

        [AdminMod]
        public ActionResult Trainers()
        {
            ViewBag.ActivePage = "Trainers";
            return View();
        }

        public ActionResult Products()
        {
            ViewBag.ActivePage = "Products";
            return View();                   
        }
        public ActionResult Users()
        {
            ViewBag.ActivePage = "Users";
            return View();                   
        }
        public ActionResult Orders()
        {
            ViewBag.ActivePage = "Orders";
            return View();                   
        }
    }
}