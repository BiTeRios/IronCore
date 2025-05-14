using IronCore.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IronCore.Areas.Admin.Controllers
{
    public class UsersController : BaseAdminController
    {
        public ActionResult Index()
            => View(_userBL.GetAllUsers());

        public ActionResult Edit(int id)
            => View(_userBL.GetById(id));

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(UserDbModel model)
        {
            if (!ModelState.IsValid) return View(model);
            _userBL.Update(model);
            TempData["msg"] = "Пользователь сохранён";
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
            => View(_userBL.GetById(id));

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _userBL.Delete(id);
            TempData["msg"] = "Пользователь удалён";
            return RedirectToAction("Index");
        }
    }

}