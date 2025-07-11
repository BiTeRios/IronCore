﻿using IronCore.Domain.Entities.Product;
using IronCore.Domain.Entities.User;
using IronCore.Domain.Enums.Order;
using IronCore.Domain.Enums.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IronCore.Domain.Entities.Order
{
    public class OrderDbModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Display(Name = "Created")]
        [DataType(DataType.Date)]
        public DateTime Created { get; set; } = DateTime.Now;
        [Display(Name = "State")]
        public UState State { get; set; }
        [Display(Name = "Total")]
        public decimal Total { get; set; }
        [Display(Name = "Products")]
        public virtual ICollection<ProductDbModel> Products { get; set; }
        [Display(Name = "Owner")]
        [ForeignKey("User")]
        public int UserId { get; set; }

        public virtual UserDbModel User { get; set; }

    }
}
