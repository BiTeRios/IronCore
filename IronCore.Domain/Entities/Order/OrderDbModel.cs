using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IronCore.Domain.Entities.Order
{
    public class OrderDbModel
    {
        [Key] 
        public int Id { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public decimal Total { get; set; }
        public virtual ICollection<OrderItemDbModel> Items { get; set; } =
            new List<OrderItemDbModel>();
    }
}
