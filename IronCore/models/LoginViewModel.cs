using System;
using System.ComponentModel.DataAnnotations;

namespace IronCore.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Введите имя пользователя")]
        [Display(Name = "Login")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
