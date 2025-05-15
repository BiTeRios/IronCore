using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IronCore.Models
{
    public class CartViewModel
    {
        public IEnumerable<ProductViewModel> Items { get; set; }
        public decimal Total { get; set; }
    }
}