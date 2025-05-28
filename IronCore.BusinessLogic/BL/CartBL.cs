using IronCore.BusinessLogic.Core;
using IronCore.BusinessLogic.Interfaces;
using IronCore.Domain.Entities.Cart;
using IronCore.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IronCore.BusinessLogic.BL
{
    public class CartBL : CartApi, ICart
    {
        public IEnumerable<CartDTO> GetCartItems()
        {
            return GetAllCartsAPI().Select(MapToDTO).ToList();
        }

        public CartDTO GetCurrentCart()
        {
            var carts = GetAllCartsAPI();
            var cart = carts.FirstOrDefault();
            return cart != null ? MapToDTO(cart) : null;
        }

        public void SetCurrentCart(CartDTO cart)
        {
            var dbCart = MapToDb(cart);
            UpdateCartAPI(dbCart);
        }

        public void AddToCart(ProductDTO product)
        {
            var carts = GetAllCartsAPI();
            var cart = carts.FirstOrDefault();
            if (cart == null)
            {
                cart = new CartDbModel
                {
                    ProductsInCart = new List<ProductDbModel> { MapProductToDb(product) },
                    Price = product.Price,
                    Discount = 0
                };
                CreateCartAPI(cart);
            }
            else
            {
                cart.ProductsInCart.Add(MapProductToDb(product));
                cart.Price += product.Price;
                UpdateCartAPI(cart);
            }
        }

        public void RemoveFromCart(int productId)
        {
            var cart = GetAllCartsAPI().FirstOrDefault();
            if (cart != null)
            {
                var prod = cart.ProductsInCart.FirstOrDefault(p => p.Id == productId);
                if (prod != null)
                {
                    cart.ProductsInCart.Remove(prod);
                    cart.Price -= prod.Price;
                    UpdateCartAPI(cart);
                }
            }
        }

        public void ClearCart()
        {
            var cart = GetAllCartsAPI().FirstOrDefault();
            if (cart != null)
            {
                cart.ProductsInCart.Clear();
                cart.Price = 0;
                cart.Discount = 0;
                UpdateCartAPI(cart);
            }
        }

        public void UpdateQuantity(int productId, int newQuantity)
        {
            var cart = GetAllCartsAPI().FirstOrDefault();
            if (cart != null)
            {
                var prod = cart.ProductsInCart.FirstOrDefault(p => p.Id == productId);
                if (prod != null)
                {
                    prod.Quantity = newQuantity;
                    UpdateCartAPI(cart);
                }
            }
        }

        public decimal CalculateTotal()
        {
            var cart = GetAllCartsAPI().FirstOrDefault();
            return cart?.Price ?? 0;
        }

        public bool IsProductInCart(int productId)
        {
            var cart = GetAllCartsAPI().FirstOrDefault();
            return cart != null && cart.ProductsInCart.Any(p => p.Id == productId);
        }

        public void IncrementQuantity(int productId)
        {
            var cart = GetAllCartsAPI().FirstOrDefault();
            if (cart != null)
            {
                var prod = cart.ProductsInCart.FirstOrDefault(p => p.Id == productId);
                if (prod != null)
                {
                    prod.Quantity += 1;
                    UpdateCartAPI(cart);
                }
            }
        }

        public void DecrementQuantity(int productId)
        {
            var cart = GetAllCartsAPI().FirstOrDefault();
            if (cart != null)
            {
                var prod = cart.ProductsInCart.FirstOrDefault(p => p.Id == productId);
                if (prod != null && prod.Quantity > 1)
                {
                    prod.Quantity -= 1;
                    UpdateCartAPI(cart);
                }
            }
        }

        private CartDTO MapToDTO(CartDbModel db)
        {
            return new CartDTO
            {
                Id = db.Id,
                Price = db.Price,
                Discount = db.Discount,
                ProductsInCart = db.ProductsInCart?.Select(MapProductToDTO).ToList() ?? new List<ProductDTO>()
            };
        }

        private CartDbModel MapToDb(CartDTO dto)
        {
            return new CartDbModel
            {
                Id = dto.Id,
                Price = dto.Price,
                Discount = dto.Discount,
                ProductsInCart = dto.ProductsInCart?.Select(MapProductToDb).ToList() ?? new List<ProductDbModel>()
            };
        }

        private ProductDTO MapProductToDTO(ProductDbModel db)
        {
            return new ProductDTO
            {
                Id = db.Id,
                Title = db.Title,
                Description = db.Description,
                Price = db.Price,
                Quantity = db.Quantity,
                ImageUrl = db.ImageUrl
            };
        }

        private ProductDbModel MapProductToDb(ProductDTO dto)
        {
            return new ProductDbModel
            {
                Id = dto.Id,
                Title = dto.Title,
                Description = dto.Description,
                Price = dto.Price,
                Quantity = dto.Quantity,
                ImageUrl = dto.ImageUrl
            };
        }
    }
}
