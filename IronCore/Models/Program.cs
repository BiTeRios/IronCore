using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IronCore.Models
{
	public class Program
	{
        [Key]
        public int ProgramId { get; set; }

        [Required(ErrorMessage = "Введите название программы")]
        [StringLength(100)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Введите краткое описание программы")]
        [StringLength(500)]
        public string ShortDescription { get; set; }

        public string FullDescription { get; set; }

        [Range(0, 100000, ErrorMessage = "Цена не может быть отрицательной")]
        public decimal Price { get; set; }

        public string Category { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}