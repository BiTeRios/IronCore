using IronCore.BusinessLogic.Interfaces;
using IronCore.Domain.Entities.Cart;
using IronCore.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IronCore.BusinessLogic.BL
{
    public sealed class CartBL : ICart
    {
        private CartItem _current;
        public CartBL()
        {
            _current = new CartItem
            {
                ID = Guid.NewGuid().GetHashCode(),
                ProductsInCart = new List<ProductDbModel>(),
                Discount = 0m
            };
        }
        public CartItem GetCurrentCart() => _current;
        public void SetCurrentCart(CartItem c) => _current = c ?? _current;
        public IEnumerable<CartItem> GetCartItems() => new[] { _current };
        public bool IsProductInCart(int productId) =>
            _current.ProductsInCart.Any(p => p.ProductID == productId);
        public void AddToCart(ProductDbModel product)
        {
            if (product == null) return;

            var item = new ProductDbModel
            {
                ProductID = product.ProductID,
                ProductName = product.ProductName,   
                ImageUrl = product.ImageUrl,
                Description = product.Description,
                Price = product.Price,
                Quantity = 1                     
            };

            var existing = _current.ProductsInCart
                                   .FirstOrDefault(p => p.ProductID == item.ProductID);

            if (existing == null)
                _current.ProductsInCart.Add(item);
            else
                existing.Quantity++;
        }
        public void RemoveFromCart(int productId) =>
            _current.ProductsInCart.RemoveAll(p => p.ProductID == productId);

        public void IncrementQuantity(int productId)
        {
            var p = _current.ProductsInCart.FirstOrDefault(x => x.ProductID == productId);
            if (p != null) p.Quantity++;
        }

        public void DecrementQuantity(int productId)
        {
            var p = _current.ProductsInCart.FirstOrDefault(x => x.ProductID == productId);
            if (p == null) return;

            if (--p.Quantity <= 0) RemoveFromCart(productId);
        }

        public void UpdateQuantity(int productId, int newQuantity)
        {
            if (newQuantity <= 0) { RemoveFromCart(productId); return; }

            var p = _current.ProductsInCart.FirstOrDefault(x => x.ProductID == productId);
            if (p != null) p.Quantity = newQuantity;
        }
        public decimal CalculateTotal() =>
            _current.ProductsInCart.Sum(p => p.Price * p.Quantity) - _current.Discount;

        public void ClearCart() => _current.ProductsInCart.Clear();
    }
}
