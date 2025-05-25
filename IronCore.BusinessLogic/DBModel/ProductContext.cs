using IronCore.Domain.Entities.Coach;
using IronCore.Domain.Entities.Product;
using System.Data.Entity;

namespace IronCore.BusinessLogic.DBModel
{
    public class ProductContext : DbContext
    {
        public ProductContext() : base("name=IronCore") { }
        public DbSet<ProductDbModel> Products { get; set; }
    }
}
