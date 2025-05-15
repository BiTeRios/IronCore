using IronCore.Domain.Entities.Product;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IronCore.Domain.Entities.Order
{
    [Table("OrderItems")]
    public class OrderItemDbModel
    {
        [Key] public int OrderItemID { get; set; }

        /* FK → Order */
        public int OrderID { get; set; }
        public virtual OrderDbModel Order { get; set; }

        /* копируем данные товара «как было» */
        public int ProductID { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
