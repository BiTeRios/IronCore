using System.Collections.Generic;

namespace IronCore.Models
{
    public class CartViewModel
    {
        public int Id { get; set; }
        public List<ProductViewModel> Products { get; set; } = new List<ProductViewModel>();
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal Total => Price - Discount;
    }
}
