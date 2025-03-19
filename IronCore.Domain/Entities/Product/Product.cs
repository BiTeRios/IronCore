using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using IronCore.Domain.Entities;

namespace IronCore.Domain.Entities.Product
{
    public class ProductItem
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool InCart { get; set; }
        public int Quantity { get; set; }
        public string ImagePath { get; set; }
        public string BrandOrTrainer { get; set; }
        public string ShortDescription { get; set; }
        public DateTime DateOfCreation { get; set; }
    }
}
