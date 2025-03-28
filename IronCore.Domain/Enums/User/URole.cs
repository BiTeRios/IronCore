using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronCore.Domain.Enums.User
{
    public enum URole
    {
        User = 0,
        User1 = 1, 
        Vip = 10,

        Vip2 = 20,
        Moderator = 100,
        Administrator = 200,
        Banned = 400,
        Lost = 500,
    }
}
