using IronCore.BusinessLogic.Core;
using IronCore.BusinessLogic.Interfaces;
using IronCore.Domain.Entities.Cart;
using IronCore.Domain.Entities.Product;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IronCore.BusinessLogic.BL
{
    public class CartBL : ICart
    {
        private const string CartSessionKey = "SessionCart";

        private CartDTO GetCartFromSession()
        {
            var session = HttpContext.Current.Session;
            var json = session[CartSessionKey] as string;

            if (string.IsNullOrEmpty(json))
            {
                return new CartDTO
                {
                    ProductsInCart = new List<ProductDTO>(),
                    Price = 0,
                    Discount = 0
                };
            }

            return JsonConvert.DeserializeObject<CartDTO>(json);
        }

        private void SaveCartToSession(CartDTO cart)
        {
            var json = JsonConvert.SerializeObject(cart);
            HttpContext.Current.Session[CartSessionKey] = json;
        }

        public IEnumerable<CartDTO> GetCartItems()
        {
            return new List<CartDTO> { GetCartFromSession() };
        }

        public CartDTO GetCurrentCart()
        {
            return GetCartFromSession();
        }

        public void SetCurrentCart(CartDTO cart)
        {
            SaveCartToSession(cart);
        }

        public void AddToCart(ProductDTO product)
        {
            var cart = GetCartFromSession();
            var existing = cart.ProductsInCart.FirstOrDefault(p => p.Id == product.Id);
            if (existing != null)
            {
                existing.Quantity += 1;
            }
            else
            {
                product.Quantity = 1;
                cart.ProductsInCart.Add(product);
            }
            cart.Price += product.Price;
            SaveCartToSession(cart);
        }

        public void RemoveFromCart(int productId)
        {
            var cart = GetCartFromSession();
            var item = cart.ProductsInCart.FirstOrDefault(p => p.Id == productId);
            if (item != null)
            {
                cart.Price -= item.Price * item.Quantity;
                cart.ProductsInCart.Remove(item);
            }
            SaveCartToSession(cart);
        }

        public void ClearCart()
        {
            HttpContext.Current.Session.Remove(CartSessionKey);
        }

        public void UpdateQuantity(int productId, int newQuantity)
        {
            var cart = GetCartFromSession();
            var prod = cart.ProductsInCart.FirstOrDefault(p => p.Id == productId);
            if (prod != null)
            {
                cart.Price -= prod.Price * prod.Quantity;
                prod.Quantity = newQuantity;
                cart.Price += prod.Price * prod.Quantity;
            }
            SaveCartToSession(cart);
        }

        public decimal CalculateTotal()
        {
            var cart = GetCartFromSession();
            return cart.Price;
        }

        public bool IsProductInCart(int productId)
        {
            var cart = GetCartFromSession();
            return cart.ProductsInCart.Any(p => p.Id == productId);
        }

        public void IncrementQuantity(int productId)
        {
            var cart = GetCartFromSession();
            var prod = cart.ProductsInCart.FirstOrDefault(p => p.Id == productId);
            if (prod != null)
            {
                prod.Quantity += 1;
                cart.Price += prod.Price;
            }
            SaveCartToSession(cart);
        }

        public void DecrementQuantity(int productId)
        {
            var cart = GetCartFromSession();
            var prod = cart.ProductsInCart.FirstOrDefault(p => p.Id == productId);
            if (prod != null && prod.Quantity > 1)
            {
                prod.Quantity -= 1;
                cart.Price -= prod.Price;
            }
            SaveCartToSession(cart);
        }
    }
}
