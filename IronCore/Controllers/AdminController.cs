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
            return View();                   // Views/Admin/Index.cshtml
        }

        [AdminMod]
        public ActionResult Trainers() => View();
    }
}