using IronCore.BusinessLogic.Interfaces;
using IronCore.Domain.Entities.Coach;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronCore.BusinessLogic.Core;
using IronCore.BusinessLogic.DBModel;
using IronCore.Domain.Entities.Product;

namespace IronCore.BusinessLogic.BL
{
    public class CoachBL : ICoach
    {
        private readonly CoachContext ctx = new CoachContext();

        public CoachCl getInfoAboutCoach(int id) => ctx.Coaches.Find(id);
        public IEnumerable<CoachCl> getAllCoaches() => ctx.Coaches.ToList();
        public bool deleteCoach(int id)
        {
            var c = ctx.Coaches.Find(id);
            if (c is null) return false;
            ctx.Coaches.Remove(c);
            ctx.SaveChanges();
            return true;
        }

        public void addCoach(CoachCl coach)
        {
            ctx.Coaches.Add(coach);
            ctx.SaveChanges();
        }

        public void modifyCoach(CoachCl coach)
        {
            var current = ctx.Coaches.Find(coach.ID);
            if (current is null) return;
            ctx.Entry(current).CurrentValues.SetValues(coach);
            ctx.SaveChanges();
        }
    }

}
