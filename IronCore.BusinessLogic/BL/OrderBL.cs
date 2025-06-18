using IronCore.BusinessLogic.Core;
using IronCore.BusinessLogic.Interfaces;
using IronCore.Domain.Entities.Order;
using IronCore.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IronCore.BusinessLogic.BL
{
    public class OrderBL : OrderApi, IOrder
    {
        public List<OrderDTO> GetAllOrders()
        {
            return GetAllOrdersAPI().Select(MapToOrder).ToList();
        }

        public OrderDTO GetOrderById(int id)
        {
            var dbOrder = GetOrderByIdAPI(id);
            return dbOrder != null ? MapToOrder(dbOrder) : null;
        }

        public bool CreateOrder(OrderDTO newOrder)
        {
            if (newOrder == null || newOrder.Products == null || !newOrder.Products.Any())
                return false;

            var dbOrder = MapToDb(newOrder);
            CreateOrderAPI(dbOrder);
            return true;
        }

        public bool DeleteOrder(int id)
        {
            if (id <= 0) return false;
            try
            {
                DeleteOrderAPI(id);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool ChangeOrderState(int orderId, IronCore.Domain.Enums.Order.UState newState)
        {
            if (orderId <= 0) return false;
            return ChangeOrderStateAPI(orderId, newState);
        }
        public List<OrderDTO> GetOrdersByDate(DateTime startDate, DateTime endDate)
        {
            return GetOrdersByDateAPI(startDate, endDate)
                .Select(MapToOrder)
                .ToList();
        }

        private OrderDTO MapToOrder(OrderDbModel db)
        {
            return new OrderDTO
            {
                Id = db.Id,
                UserId = db.UserId,
                Created = db.Created,
                State = db.State,
                Total = db.Total,
                Products = db.Products 
            };
        }

        private OrderDbModel MapToDb(OrderDTO order)
        {
            return new OrderDbModel
            {
                Id = order.Id,
                UserId = order.UserId,
                Created = order.Created,
                State = order.State,
                Total = order.Total,
                Products = order.Products
            };
        }
    }
}
