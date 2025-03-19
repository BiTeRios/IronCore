using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronCore.Domain.Entities.Product
{
    class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public DateTime DateOfCreation { get; set; }
        public string Description { get; set; }
    }
}
