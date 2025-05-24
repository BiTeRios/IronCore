using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronCore.Domain.Entities.User.Registration
{
    public class UserRegistrationResult
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public UserDbModel User { get; set; }
    }
}
