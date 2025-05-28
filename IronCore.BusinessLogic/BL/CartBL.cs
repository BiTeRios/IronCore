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
        private CartDbModel _current;
        public CartBL()
        {
            _current = new CartDbModel
            {
                Id = Guid.NewGuid().GetHashCode(),
                ProductsInCart = new List<ProductDbModel>(),
                Discount = 0m
            };
        }
        public CartDbModel GetCurrentCart() => _current;
        public void SetCurrentCart(CartDbModel c) => _current = c ?? _current;
        public IEnumerable<CartDbModel> GetCartItems() => new[] { _current };
        public bool IsProductInCart(int productId) =>
            _current.ProductsInCart.Any(p => p.Id == productId);
        public void AddToCart(ProductDbModel product)
        {
            if (product == null) return;

            var item = new ProductDbModel
            {
                Id = product.Id,
                ProductName = product.ProductName,   
                ImageUrl = product.ImageUrl,
                Description = product.Description,
                Price = product.Price,
                Quantity = 1                     
            };

            var existing = _current.ProductsInCart
                                   .FirstOrDefault(p => p.Id == item.Id);

            if (existing == null)
                _current.ProductsInCart.Add(item);
            else
                existing.Quantity++;
        }
        public void RemoveFromCart(int productId) =>
            _current.ProductsInCart.RemoveAll(p => p.Id == productId);

        public void IncrementQuantity(int productId)
        {
            var p = _current.ProductsInCart.FirstOrDefault(x => x.Id == productId);
            if (p != null) p.Quantity++;
        }

        public void DecrementQuantity(int productId)
        {
            var p = _current.ProductsInCart.FirstOrDefault(x => x.Id == productId);
            if (p == null) return;

            if (--p.Quantity <= 0) RemoveFromCart(productId);
        }

        public void UpdateQuantity(int productId, int newQuantity)
        {
            if (newQuantity <= 0) { RemoveFromCart(productId); return; }

            var p = _current.ProductsInCart.FirstOrDefault(x => x.Id == productId);
            if (p != null) p.Quantity = newQuantity;
        }
        public decimal CalculateTotal() =>
            _current.ProductsInCart.Sum(p => p.Price * p.Quantity) - _current.Discount;

        public void ClearCart() => _current.ProductsInCart.Clear();
    }
}
