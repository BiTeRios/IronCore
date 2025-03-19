using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronCore.Domain.Entities.Product;

namespace IronCore.Domain.Entities.Cart
{
    public class CartItem
    {
        public List<ProductItem> ProductsInCart { get; set; }
        public int CartID { get; set; }
        public decimal Price { get; set; } = 0;
        public decimal Discount { get; set; } = 0;
    }
}
