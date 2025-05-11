using System.ComponentModel.DataAnnotations;

namespace IronCore.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Введите имя пользователя или e-mail")]
        [Display(Name = "Логин или Email")]
        public string Credential { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить меня")]
        public bool RememberMe { get; set; }
    }
}
