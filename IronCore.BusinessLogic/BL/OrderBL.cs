using IronCore.BusinessLogic.DBModel;
using IronCore.Domain.Entities.Cart;
using IronCore.Domain.Entities.Order;
using System.Linq;

namespace IronCore.BusinessLogic.BL
{
    public class OrderBL
    {
        public int CreateOrder(CartItem cart)
        {
            if (cart == null || !cart.ProductsInCart.Any()) return 0;

            var ctx = new UserContext();

            var order = new OrderDbModel
            {
                Total = cart.ProductsInCart.Sum(p => p.Price * p.Quantity)
            };

            foreach (var p in cart.ProductsInCart)
            {
                order.Items.Add(new OrderItemDbModel
                {
                    ProductID = p.ProductID,
                    Title = p.ProductName,
                    Price = p.Price,
                    Quantity = p.Quantity
                });
            }

            ctx.Orders.Add(order);
            ctx.SaveChanges();
            return order.OrderID;                 
        }
    }
}
