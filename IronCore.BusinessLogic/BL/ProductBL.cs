using IronCore.BusinessLogic.Core;
using IronCore.BusinessLogic.Interfaces;
using IronCore.Domain.Entities.Product;
using System.Collections.Generic;
using System.Linq;

namespace IronCore.BusinessLogic.BL
{
    public class ProductBL : ProductApi, IProduct
    {
        public ProductDTO GetProductById(int id)
        {
            var prod = GetProductByIdAPI(id);
            return prod != null ? MapToDTO(prod) : null;
        }

        public IEnumerable<ProductDTO> GetAll()
        {
            return GetAllProductsAPI().Select(MapToDTO).ToList();
        }

        public void CreateProduct(ProductDTO product)
        {
            if (string.IsNullOrWhiteSpace(product.Title) || product.Price < 0)
                return;
            CreateProductAPI(MapToDb(product));
        }

        public bool DeleteProduct(int productId)
        {
            if (productId <= 0) return false;
            return DeleteProductAPI(productId);
        }

        public void UpdateProduct(ProductDTO product)
        {
            var dbProduct = MapToDb(product);
            UpdateProductAPI(dbProduct);
        }

        private ProductDTO MapToDTO(ProductDbModel db)
        {
            return new ProductDTO
            {
                Id = db.Id,
                Title = db.Title,
                Description = db.Description,
                Price = db.Price,
                Quantity = db.Quantity,
                ImageUrl = db.ImageUrl,
                IsVisibleInCatalog = db.IsVisibleInCatalog
            };
        }

        private ProductDbModel MapToDb(ProductDTO dto)
        {
            return new ProductDbModel
            {
                Id = dto.Id,
                Title = dto.Title,
                Description = dto.Description,
                Price = dto.Price,
                Quantity = dto.Quantity,
                ImageUrl = dto.ImageUrl,
                IsVisibleInCatalog = dto.IsVisibleInCatalog
            };
        }
    }
}
