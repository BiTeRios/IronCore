using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronCore.Domain.Entities.User;
using IronCore.BusinessLogic.DBModel;

namespace IronCore.BusinessLogic.Core
{
    public class UserApi
    {
        private readonly UserContext _context; 

        public UserApi()
        {
            _context = new UserContext();
        }

        public ULoginData Login(string email, string password, string username)
        {
            return _context.Users
                           .FirstOrDefault(u => (u.Credential == email || u.Credential == username)
                                             && u.Password == password);
        }
    }
}
