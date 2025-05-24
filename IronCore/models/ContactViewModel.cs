using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IronCore.Models
{
	public class ContactViewModel
	{
        public int Id { get; set; }
        [Required(ErrorMessage = "Укажите имя")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Укажите почту")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Укажите номер")]
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
    }
}