using IronCore.Domain.Entities.Cart;
using IronCore.Domain.Entities.Coach;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronCore.BusinessLogic.DBModel
{
    public class CartContext : DbContext
    {
        public CartContext() : base("name=IronCore") { }
        public DbSet<CartDbModel> Carts { get; set; }
    }
}
