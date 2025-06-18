using IronCore.Domain.Entities.Coach;
using IronCore.BusinessLogic.Interfaces;
using IronCore.BusinessLogic.Core;
using System.Collections.Generic;
using System.Linq;
using IronCore.BusinessLogic.DBModel;
using System.Data.Entity;

namespace IronCore.BusinessLogic.BL
{
    public class CoachBL : CoachApi, ICoach
    {
        public List<CoachDTO> GetAllCoaches()
        {
            return GetAllCoachesAPI().Select(MapToCoach).ToList();
        }

        public CoachDTO GetCoachById(int id)
        {
            var coach = GetCoachByIdAPI(id);
            return coach != null ? MapToCoach(coach) : null;
        }

        public bool CreateCoach(CoachDTO newCoach)
        {
            if (string.IsNullOrWhiteSpace(newCoach.FullName) || newCoach.ExperienceTime < 0)
                return false;

            var dbCoach = MapToDB(newCoach);
            CreateCoachAPI(dbCoach);
            return true;
        }

        public bool DeleteCoach(int id)
        {
            if (id <= 0) return false;

            try
            {
                DeleteCoachAPI(id);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool UpdateCoach(CoachDTO coach)
        {
            if (coach == null || coach.Id <= 0) return false;

            var existingCoach = GetCoachByIdAPI(coach.Id);
            if (existingCoach == null) return false;

            existingCoach.FullName = coach.FullName;
            existingCoach.ImagePath = coach.ImagePath;
            existingCoach.ExperienceTime = coach.ExperienceTime;
            existingCoach.Qualification = coach.Qualification;
            existingCoach.Specialization = coach.Specialization;
            existingCoach.Bio = coach.Bio;
            existingCoach.Testimonials = coach.Testimonials;
            existingCoach.TelegramUrl = coach.TelegramUrl;
            existingCoach.SteamUrl = coach.SteamUrl;
            existingCoach.InstagramUrl = coach.InstagramUrl;
            UpdateCoachApi(existingCoach);
            return true;
        }

        public List<CoachDTO> GetCoachesSortedByExperience(bool descending = true)
        {
            var coaches = GetAllCoachesAPI();
            var sorted = descending
                ? coaches.OrderByDescending(c => c.ExperienceTime)
                : coaches.OrderBy(c => c.ExperienceTime);

            return sorted.Select(MapToCoach).ToList();
        }

        private CoachDbModel MapToDB(CoachDTO coach)
        {
            return new CoachDbModel
            {
                Id = coach.Id,
                ImagePath = coach.ImagePath,
                FullName = coach.FullName,
                ExperienceTime = coach.ExperienceTime,
                Qualification = coach.Qualification,
                Specialization = coach.Specialization,
                Bio = coach.Bio,
                Testimonials = coach.Testimonials,
                TelegramUrl = coach.TelegramUrl,
                SteamUrl = coach.SteamUrl,
                InstagramUrl = coach.InstagramUrl
            };
        }

        private CoachDTO MapToCoach(CoachDbModel db)
        {
            return new CoachDTO
            {
                Id = db.Id,
                ImagePath = db.ImagePath,
                FullName = db.FullName,
                ExperienceTime = db.ExperienceTime,
                Qualification = db.Qualification,
                Specialization = db.Specialization,
                Bio = db.Bio,
                Testimonials = db.Testimonials,
                TelegramUrl = db.TelegramUrl,
                SteamUrl = db.SteamUrl,
                InstagramUrl = db.InstagramUrl
            };
        }
    }
}
