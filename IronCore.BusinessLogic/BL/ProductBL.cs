using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronCore.BusinessLogic.Core;
using IronCore.BusinessLogic.Interfaces;
using IronCore.Domain.Entities.Product;

namespace IronCore.BusinessLogic.BL
{
    public class ProductBL : IProduct
    {
        ProductItem IProduct.GetProductById(int productId)
        {
            throw new NotImplementedException();
        }
        public void CreateProduct(ProductItem product)
        {
            throw new NotImplementedException();
        }

        public bool DeleteProduct(int productId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductItem> GetAllProducts()
        {
            throw new NotImplementedException();
        }
        public void UpdateProduct(ProductItem product)
        {
            throw new NotImplementedException();
        }
    }
}
