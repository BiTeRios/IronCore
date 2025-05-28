using IronCore.BusinessLogic.DBModel;
using IronCore.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace IronCore.BusinessLogic.Core
{
    public class OrderApi
    {
        public List<OrderDbModel> GetAllOrdersAPI()
        {
            using (var context = new OrderContext())
            {
                return context.Orders
                    .Include(o => o.Products)
                    .ToList();
            }
        }

        public OrderDbModel GetOrderByIdAPI(int id)
        {
            using (var context = new OrderContext())
            {
                return context.Orders
                    .Include(o => o.Products)
                    .FirstOrDefault(o => o.Id == id);
            }
        }

        public void CreateOrderAPI(OrderDbModel newOrder)
        {
            using (var context = new OrderContext())
            {
                context.Orders.Add(newOrder);
                context.SaveChanges();
            }
        }

        public void DeleteOrderAPI(int id)
        {
            using (var context = new OrderContext())
            {
                var order = context.Orders.Include(o => o.Products).FirstOrDefault(o => o.Id == id);
                if (order == null)
                    throw new ArgumentException("Order not found");

                context.Orders.Remove(order);
                context.SaveChanges();
            }
        }
        public bool ChangeOrderStateAPI(int orderId, IronCore.Domain.Enums.Order.UState newState)
        {
            using (var context = new OrderContext())
            {
                var order = context.Orders.Find(orderId);
                if (order == null) return false;
                order.State = newState;
                context.SaveChanges();
                return true;
            }
        }
        public List<OrderDbModel> GetOrdersByDateAPI(DateTime startDate, DateTime endDate)
        {
            using (var context = new OrderContext())
            {
                return context.Orders
                    .Include(o => o.Products)
                    .Where(o => o.Created >= startDate && o.Created <= endDate)
                    .ToList();
            }
        }


    }
}
