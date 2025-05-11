using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace IronCore.Models
{
    public class RegisterViewModel
    {
        [Required, StringLength(30, MinimumLength = 5)]
        public string UserName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required, MinLength(6), DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DataType(DataType.Password), Compare("Password",
            ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
    }
}