using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IronCore.Models
{
	public class ShoppingCart
	{
        [Key]
        public int CartId { get; set; }

        public int UserId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public virtual ICollection<CartItem> Items { get; set; }

    }
    public class CartItem
    {
        [Key]
        public int CartItemId { get; set; }

        public int CartId { get; set; }

        public int ProgramId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}