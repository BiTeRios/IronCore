using IronCore.BusinessLogic.Core;
using IronCore.BusinessLogic.Interfaces;
using IronCore.Domain.Entities.Cart;
using IronCore.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronCore.BusinessLogic.BL
{
    public class CartBL : UserApi, ICart
    {
        void ICart.AddToCart(ProductItem product)
        {
            throw new NotImplementedException();
        }
        decimal ICart.CalculateTotal()
        {
            throw new NotImplementedException();
        }
        void ICart.ClearCart()
        {
            throw new NotImplementedException();
        }
        void ICart.DecrementQuantity(int productId)
        {
            throw new NotImplementedException();
        }
        IEnumerable<CartItem> ICart.GetCartItems()
        {
            throw new NotImplementedException();
        }
        CartItem ICart.GetCurrentCart()
        {
            throw new NotImplementedException();
        }

        void ICart.IncrementQuantity(int productId)
        {
            throw new NotImplementedException();
        }
        bool ICart.IsProductInCart(int productId)
        {
            throw new NotImplementedException();
        }
        void ICart.RemoveFromCart(int productId)
        {
            throw new NotImplementedException();
        }
        void ICart.SetCurrentCart(CartItem cart)
        {
            throw new NotImplementedException();
        }
        void ICart.UpdateQuantity(int productId, int newQuantity)
        {
            throw new NotImplementedException();
        }
    }
}
