using IronCore.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronCore.Domain.Enums.Order;

namespace IronCore.Domain.Entities.Order
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public UState State { get; set; }
        public decimal Total { get; set; }
        public virtual ICollection<ProductDbModel> Products { get; set; } =
            new List<ProductDbModel>();

        public int UserId { get; set; }
    }
}
