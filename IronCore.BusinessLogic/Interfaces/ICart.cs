using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronCore.Domain.Entities.Cart;
using IronCore.Domain.Entities.Product;

namespace IronCore.BusinessLogic.Interfaces
{
    interface ICart
    {
        void AddToCart(ProductItem product);
        void RemoveFromCart(int productId);
        void UpdateQuantity(int productId, int newQuantity);
        IEnumerable<CartItem> GetCartItems();
        void ClearCart();
        decimal CalculateTotal();
        bool IsProductInCart(int productId);
        CartItem GetCurrentCart();
        void SetCurrentCart(CartItem cart);
        void IncrementQuantity(int productId);
        void DecrementQuantity(int productId);
    }
}
