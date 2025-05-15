using IronCore.Areas.Admin.Models;
using IronCore.Domain.Entities.User;
using IronCore.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IronCore.Controllers.Admin
{
    [AdminMod(Order = 0)]
    [AdminMode(Order = 1)]
    public class UsersController : BaseAdminController
    {
        public ActionResult Index()
            => View(_userBL.GetAllUsers());

        // GET
        public ActionResult Edit(int id)
        {
            var u = _userBL.GetById(id);
            if (u == null) return HttpNotFound();

            var vm = new EditUserViewModel
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                PhoneNumber = u.PhoneNumber
            };
            return View(vm);
        }

        // POST
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(EditUserViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var u = _userBL.GetById(vm.Id);
            if (u == null) return HttpNotFound();

            u.UserName = vm.UserName;
            u.Email = vm.Email;
            u.FirstName = vm.FirstName;
            u.LastName = vm.LastName;
            u.PhoneNumber = vm.PhoneNumber;

            _userBL.Update(u);
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