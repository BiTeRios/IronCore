using IronCore.Domain.Entities.Coach;
using IronCore.Domain.Entities.Order;
using System.Data.Entity;

namespace IronCore.BusinessLogic.DBModel
{
    public class OrderContext : DbContext
    {
        public OrderContext() : base("name=IronCore") { }
        public DbSet<OrderDbModel> Orders { get; set; }
    }
}