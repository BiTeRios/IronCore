using IronCore.BusinessLogic.BL;
using IronCore.BusinessLogic.Interfaces;
using IronCore.Domain.Entities.Coach;
using IronCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IronCore.Controllers
{
    public class CoachController : BaseController
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