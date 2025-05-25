using IronCore.Domain.Entities.Coach;
using System.Data.Entity;

namespace IronCore.BusinessLogic.DBModel
{
    public class CoachContext : DbContext
    {
        public CoachContext() : base("name=IronCore") { }
        public DbSet<CoachDbModel> Coaches { get; set; }
    }
}