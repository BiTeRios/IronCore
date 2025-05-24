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
        void AddToCart(ProductDbModel product);
        void RemoveFromCart(int productId);
        void UpdateQuantity(int productId, int newQuantity);
        IEnumerable<CartDbModel> GetCartItems();
        void ClearCart();
        decimal CalculateTotal();
        bool IsProductInCart(int productId);
        CartDbModel GetCurrentCart();
        void SetCurrentCart(CartDbModel cart);
        void IncrementQuantity(int productId);
        void DecrementQuantity(int productId);
    }
}
