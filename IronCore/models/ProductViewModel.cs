using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IronCore.Models
{
    public class ProductViewModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }   // для корзины
    }
}