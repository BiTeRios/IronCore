using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronCore.Domain.Entities.User.Login
{
    public class UserLoginData
    {
        [Required] 
        public string Credential { get; set; }
        [Required] 
        public string Password { get; set; }
    }
}
