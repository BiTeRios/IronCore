using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronCore.Domain.Entities.Product;

namespace IronCore.BusinessLogic.Interfaces
{
    interface IProduct
    {
        ProductItem GetProductById(int productId);
        IEnumerable<ProductItem> GetAllProducts();
        void CreateProduct(ProductItem product);
        bool DeleteProduct(int productId);
        void UpdateProduct(ProductItem product);
    }
}
