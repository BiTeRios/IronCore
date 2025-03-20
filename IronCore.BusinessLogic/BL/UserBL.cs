using IronCore.BusinessLogic.Core;
using IronCore.BusinessLogic.Interfaces;
using IronCore.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronCore.BusinessLogic.BL
{
    class UserBL : UserApi, IUser
    {
        public bool AddUser(UserCl user)
        {
            throw new NotImplementedException();
        }

        public void DeactivateUser(int userId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteUser(int userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserCl> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public UserCl GetUserById(int userId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateUser(UserCl user)
        {
            throw new NotImplementedException();
        }

        public bool UserExists(int userId)
        {
            throw new NotImplementedException();
        }

        public bool ValidateCredentials(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
