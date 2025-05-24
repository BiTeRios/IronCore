using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronCore.Domain.Entities.User.Login
{
    public class UserLoginResult
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public UserDTO UserDTO { get; set; }
    }
}
