using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace IronCore.Models
{
    public class LoginViewModel
    {
        [Required, EmailAddress]
        public string Credential { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }    // <— здесь

        public bool RememberMe { get; set; }
    }
}
