using IronCore.BusinessLogic.BL;
using IronCore.BusinessLogic.Interfaces;
using IronCore.Domain.Entities.Coach;
using IronCore.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IronCore.Controllers
{
    public class CoachController : Controller
    {
        private readonly ICoach _coach;
        public CoachController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _coach = bl.GetCoachBL();
        }

        // GET: Coach
        public ActionResult Trainers()
        {
            ViewBag.ActivePage = "Trainers";
            var coaches = _coach.GetAllCoaches();
            var viewModels = coaches.Select(MapToViewModel).ToList();
            return View(viewModels);
        }

        // GET: Coach/Details/5
        public ActionResult Coach(int id)
        {
            var coach = _coach.GetCoachById(id);
            if (coach == null) return HttpNotFound();
            return View(MapToViewModel(coach));
        }

        // GET: Coach/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Coach/Create
        [HttpPost]
        public ActionResult Create(CoachViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var dto = MapToDTO(model);
            _coach.CreateCoach(dto);
            return RedirectToAction("Index");
        }

        // GET: Coach/Edit/5
        public ActionResult Edit(int id)
        {
            var coach = _coach.GetCoachById(id);
            if (coach == null) return HttpNotFound();
            return View(MapToViewModel(coach));
        }

        // POST: Coach/Edit/5
        [HttpPost]
        public ActionResult Edit(CoachViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var dto = MapToDTO(model);
            _coach.UpdateCoach(dto);
            return RedirectToAction("Index");
        }

        // GET: Coach/Delete/5
        public ActionResult Delete(int id)
        {
            var coach = _coach.GetCoachById(id);
            if (coach == null) return HttpNotFound();
            return View(MapToViewModel(coach));
        }

        // POST: Coach/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _coach.DeleteCoach(id);
            return RedirectToAction("Index");
        }

        private CoachViewModel MapToViewModel(CoachDTO dto)
        {
            return new CoachViewModel
            {
                Id = dto.Id,
                ImagePath = dto.ImagePath,
                FullName = dto.FullName,
                ExperienceTime = dto.ExperienceTime,
                Qualification = dto.Qualification,
                Specialization = dto.Specialization,
                Bio = dto.Bio,
                Testimonials = dto.Testimonials,
                TelegramUrl = dto.TelegramUrl,
                SteamUrl = dto.SteamUrl,
                InstagramUrl = dto.InstagramUrl
            };
        }

        private CoachDTO MapToDTO(CoachViewModel model)
        {
            return new CoachDTO
            {
                Id = model.Id,
                ImagePath = model.ImagePath,
                FullName = model.FullName,
                ExperienceTime = model.ExperienceTime,
                Qualification = model.Qualification,
                Specialization = model.Specialization,
                Bio = model.Bio,
                Testimonials = model.Testimonials,
                TelegramUrl = model.TelegramUrl,
                SteamUrl = model.SteamUrl,
                InstagramUrl = model.InstagramUrl
            };
        }
    }
}