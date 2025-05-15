using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronCore.Domain.Entities.Product;

namespace IronCore.BusinessLogic.Interfaces
{
    public interface IProduct
    {
        ProductDbModel GetProductById(int id);
        IEnumerable<ProductDbModel> GetAll();
        void CreateProduct(ProductDbModel product);
        bool DeleteProduct(int productId);
        void UpdateProduct(ProductDbModel product);
    }
}
