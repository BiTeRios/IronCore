using IronCore.BusinessLogic.DBModel;
using IronCore.Domain.Entities.Product;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace IronCore.BusinessLogic.Core
{
    public class ProductApi
    {
        public ProductDbModel GetProductByIdAPI(int id)
        {
            using (var context = new ProductContext())
            {
                return context.Products.FirstOrDefault(p => p.Id == id);
            }
        }

        public List<ProductDbModel> GetAllProductsAPI()
        {
            using (var context = new ProductContext())
            {
                return context.Products.ToList();
            }
        }

        public void CreateProductAPI(ProductDbModel product)
        {
            using (var context = new ProductContext())
            {
                context.Products.Add(product);
                context.SaveChanges();
            }
        }

        public bool DeleteProductAPI(int productId)
        {
            using (var context = new ProductContext())
            {
                var prod = context.Products.Find(productId);
                if (prod == null) return false;

                context.Products.Remove(prod);
                context.SaveChanges();
                return true;
            }
        }

        public void UpdateProductAPI(ProductDbModel product)
        {
            using (var context = new ProductContext())
            {
                context.Entry(product).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
