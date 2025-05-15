using IronCore.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronCore.Domain.Entities.Cart
{
    public class CartLine
    {
        public ProductDbModel Product { get; set; }
        public int Quantity { get; set; } = 1;
    }
}
