using IronCore.Domain.Entities.Coach;
using IronCore.BusinessLogic.DBModel; 
using System.Collections.Generic;
using System.Linq;
using System;
using System.Data.Entity;

namespace IronCore.BusinessLogic.Core
{
    public class CoachApi
    {
        public List<CoachDbModel> GetAllCoachesAPI()
        {
            using (var context = new CoachContext())
            {
                return context.Coaches.ToList();
            }
        }

        public CoachDbModel GetCoachByIdAPI(int id)
        {
            using (var context = new CoachContext())
            {
                return context.Coaches.FirstOrDefault(c => c.Id == id);
            }
        }

        public void CreateCoachAPI(CoachDbModel coach)
        {
            using (var context = new CoachContext())
            {
                context.Coaches.Add(coach);
                context.SaveChanges();
            }
        }

        public void DeleteCoachAPI(int id)
        {
            using (var context = new CoachContext())
            {
                var coach = context.Coaches.Find(id);
                if (coach == null) throw new ArgumentException("Coach not found");

                context.Coaches.Remove(coach);
                context.SaveChanges();
            }
        }

        public void UpdateCoachApi(CoachDbModel coach)
        {
            using (var context = new CoachContext())
            {
                context.Entry(coach).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
