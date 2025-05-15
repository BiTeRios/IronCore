using IronCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IronCore.Controllers.Admin
{
    [AdminMod(Order = 0)]
    [AdminMode(Order = 1)]
    public class AdminController : Controller
    {
        // Рубильник: включает/выключает режим для текущей сессии
        public ActionResult Toggle()
        {
            bool flag = !(Session["IsAdminMode"] as bool? ?? false);
            Session["IsAdminMode"] = flag;
            TempData["msg"] = flag ? "Admin-mode ON" : "Admin-mode OFF";
            return RedirectToAction("Index", "Home");   // куда вернуться
        }

        // «Дашборд» — можно просто перенаправить на Users
        public ActionResult Index() =>
            RedirectToAction("Index", "Users"); // из той же папки Controllers/Admin
    }
}
