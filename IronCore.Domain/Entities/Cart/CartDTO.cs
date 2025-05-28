using IronCore.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronCore.Domain.Entities.Cart
{
    public class CartDTO
    {
        public int Id { get; set; }
        public List<ProductDbModel> ProductsInCart { get; set; }
        public decimal Price { get; set; } = 0;
        public decimal Discount { get; set; } = 0;
    }
}
