using System.Web.Mvc;
using IronCore.Filters;
using IronCore.Models;
using IronCore.BusinessLogic;
using IronCore.BusinessLogic.BL;
using System.Linq;
using System.IO;
using System.Web;

namespace IronCore.Controllers
{
    [AdminOnly]
    public class AdminController : Controller
    {
        private readonly CoachBL _bl = new CoachBL();
        [AdminOnly]
        public ActionResult Index()
        {
            ViewBag.ActivePage = "Index";
            return View();                   // Views/Admin/Index.cshtml
        }

        [AdminOnly]
        public ActionResult Trainers()
        {
            ViewBag.ActivePage = "Trainers";
            var list = _bl.getAllCoaches()
                      .Select(c => new CoachViewModel
                      {
                          ID = c.Id,
                          FullName = c.FullName,
                          Specialization = c.Specialization,
                          ExperienceTime = c.ExperienceTime,
                          ImagePath = c.ImagePath
                      })
                      .ToList();
            return View(list);
        }

        // ───── GET: /Admin/Create ────────────────────────────────────────────────────
        [AdminOnly]
        public ActionResult CreateTrain()
        {
            ViewBag.ActivePage = "Trainers";
            var model = new CoachViewModel();
            return View(model);
        }

        // ───── POST: /Admin/Create ───────────────────────────────────────────────────
        [HttpPost, ValidateAntiForgeryToken]
        [AdminOnly]
        public ActionResult CreateTrain(CoachViewModel model, HttpPostedFileBase photo)
        {
            ViewBag.ActivePage = "Trainers";

            if (!ModelState.IsValid)
                return View(model);

            if (photo != null && photo.ContentLength > 0)
            {
                var folder = "~/Images/Trainers";
                var serverPath = Server.MapPath(folder);
                if (!Directory.Exists(serverPath))
                    Directory.CreateDirectory(serverPath);

                var fileName = Path.GetFileName(photo.FileName);
                var path = Path.Combine(serverPath, fileName);
                photo.SaveAs(path);
                model.ImagePath = VirtualPathUtility.Combine(folder, fileName);
            }
            var coach = new Domain.Entities.Coach.CoachDbModel
            {
                Id = model.ID,
                FullName = model.FullName,
                Specialization = model.Specialization,
                ExperienceTime = model.ExperienceTime,
                ImagePath = model.ImagePath
            };
            _bl.addCoach(coach);

            return RedirectToAction("Trainers");
        }

        // GET: /Admin/Delete/5
        [AdminOnly]
        public ActionResult DeleteTrain(int id)
        {
            var coach = _bl.getInfoAboutCoach(id);
            if (coach == null)
                return HttpNotFound();

            var vm = new CoachViewModel
            {
                ID = coach.Id,
                FullName = coach.FullName,
                Specialization = coach.Specialization,
                ExperienceTime = coach.ExperienceTime,
                ImagePath = coach.ImagePath
            };
            ViewBag.ActivePage = "Trainers";
            return View(vm);
        }

        // POST: /Admin/Delete/5
        [HttpPost, ValidateAntiForgeryToken]
        [AdminOnly]
        public ActionResult DeleteConfirmed(int id)
        {
            _bl.deleteCoach(id);
            return RedirectToAction("Trainers");
        }

        // GET: /Admin/Edit/5
        [AdminOnly]
        public ActionResult Edit(int id)
        {
            ViewBag.ActivePage = "Trainers";
            var c = _bl.getInfoAboutCoach(id);
            if (c == null) return HttpNotFound();

            var vm = new CoachViewModel
            {
                FullName = c.FullName,
                Qualification = c.Qualification,
                Specialization = c.Specialization,
                ExperienceTime = c.ExperienceTime,
                Bio = c.Bio,
                Testimonials = c.Testimonials,
                TelegramUrl = c.TelegramUrl,
                InstagramUrl = c.InstagramUrl,
                SteamUrl = c.SteamUrl,
                ImagePath = c.ImagePath
            };
            return View(vm);
        }

        // POST: /Admin/Edit/5
        [AdminOnly]
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult EditTrain(CoachViewModel model, HttpPostedFileBase photo)
        {
            ViewBag.ActivePage = "Trainers";
            if (!ModelState.IsValid)
                return View(model);

            // Передаём BL поток и имя файла, не трогаем Domain
            Stream stream = photo?.InputStream;
            string fileName = photo == null ? null : Path.GetFileName(photo.FileName);

            //_bl.SaveFromViewModel(model, stream, fileName);

            return RedirectToAction("Trainers");
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