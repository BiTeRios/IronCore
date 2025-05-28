using IronCore.BusinessLogic.DBModel;
using IronCore.Domain.Entities.Cart;
using IronCore.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace IronCore.BusinessLogic.Core
{
    public class CartApi
    {
        public List<CartDbModel> GetAllCartsAPI()
        {
            using (var context = new CartContext())
            {
                return context.Carts.Include(c => c.ProductsInCart).ToList();
            }
        }

        public CartDbModel GetCartByIdAPI(int id)
        {
            using (var context = new CartContext())
            {
                return context.Carts.Include(c => c.ProductsInCart).FirstOrDefault(c => c.Id == id);
            }
        }

        public void CreateCartAPI(CartDbModel newCart)
        {
            using (var context = new CartContext())
            {
                context.Carts.Add(newCart);
                context.SaveChanges();
            }
        }

        public void UpdateCartAPI(CartDbModel cart)
        {
            using (var context = new CartContext())
            {
                context.Entry(cart).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteCartAPI(int id)
        {
            using (var context = new CartContext())
            {
                var cart = context.Carts.Find(id);
                if (cart == null) throw new ArgumentException("Cart not found");
                context.Carts.Remove(cart);
                context.SaveChanges();
            }
        }
    }
}
