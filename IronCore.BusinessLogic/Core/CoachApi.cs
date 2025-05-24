using IronCore.BusinessLogic.DBModel;
using IronCore.Domain.Entities.Coach;
using System.Collections.Generic;
using System.Linq;

namespace IronCore.BusinessLogic.Core
{
    internal class CoachApi
    {
        internal List<CoachDbModel> GetAllCoachesAction()
        {
            var ctx = new CoachContext();
            return ctx.Coaches.OrderBy(c => c.FullName).ToList();
        }

        internal CoachDbModel GetCoachByIdAction(int id)
        {
            var ctx = new CoachContext();
            return ctx.Coaches.Find(id);
        }

        internal void AddCoachAction(CoachDbModel coach)
        {
            var ctx = new CoachContext();
            ctx.Coaches.Add(coach);
            ctx.SaveChanges();
        }

        internal void UpdateCoachAction(CoachDbModel coach)
        {
            var ctx = new CoachContext();
            ctx.Entry(coach).State = System.Data.Entity.EntityState.Modified;
            ctx.SaveChanges();
        }

        internal void DeleteCoachAction(int id)
        {
            var ctx = new CoachContext();
            var c = ctx.Coaches.Find(id);
            if (c == null) return;
            ctx.Coaches.Remove(c);
            ctx.SaveChanges();
        }
    }
}
