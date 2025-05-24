using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronCore.BusinessLogic.Core;
using IronCore.BusinessLogic.DBModel;
using IronCore.BusinessLogic.Interfaces;
using IronCore.Domain.Entities.Product;

namespace IronCore.BusinessLogic.BL
{
    public class ProductBL : IProduct
    {
        private readonly ProductContext ctx = new ProductContext();

        public IEnumerable<ProductDbModel> GetAll()          // каталог
        {
            var ctx = new ProductContext();
            return ctx.Products.AsNoTracking().ToList();
        }

        public ProductDbModel GetProductById(int id)                // один товар
        {
            var ctx = new ProductContext();
            return ctx.Products.AsNoTracking()
                               .FirstOrDefault(p => p.Id == id);
        }

        public void CreateProduct(ProductDbModel m)                 // админ
        {
            var ctx = new ProductContext();
            ctx.Products.Add(m);
            ctx.SaveChanges();
        }

        public bool DeleteProduct(int id)
        {
            var p = ctx.Products.Find(id);
            if (p is null) return false;
            ctx.Products.Remove(p);
            ctx.SaveChanges();
            return true;
        }

        public void UpdateProduct(ProductDbModel product)
        {
            var current = ctx.Products.Find(product.Id);
            if (current is null) return;
            ctx.Entry(current).CurrentValues.SetValues(product);
            ctx.SaveChanges();
        }
    }

}
