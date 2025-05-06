using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace IronCore.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Username or Email")]
        public string Credential { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }   

        public bool RememberMe { get; set; }
    }
}
