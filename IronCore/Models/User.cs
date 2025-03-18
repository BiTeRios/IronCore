using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IronCore.Models
{
	public class User
	{
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        [StringLength(50, ErrorMessage = "Имя не может быть длиннее 50 символов")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Введите фамилию")]
        [StringLength(50, ErrorMessage = "Фамилия не может быть длиннее 50 символов")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Введите email")]
        [EmailAddress(ErrorMessage = "Некорректный email-адрес")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Пароль должен содержать не менее 6 символов")]
        public string Password { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        public bool IsEmailConfirmed { get; set; }
    }
}