using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronCore.Domain.Entities.Cart;
using IronCore.Domain.Entities.Product;

namespace IronCore.BusinessLogic.Interfaces
{
     public interface ICart
    {
        void AddToCart(ProductDTO product);
        void RemoveFromCart(int Id);
        void UpdateQuantity(int Id, int newQuantity);
        IEnumerable<CartDTO> GetCartItems();
        void ClearCart();
        decimal CalculateTotal();
        bool IsProductInCart(int Id);
        CartDTO GetCurrentCart();
        void SetCurrentCart(CartDTO cart);
        void IncrementQuantity(int Id);
        void DecrementQuantity(int Id);
    }
}
