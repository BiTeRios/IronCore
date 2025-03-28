using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronCore.Domain.Entities.User;

namespace IronCore.BusinessLogic.Interfaces
{
    interface IUser
    {
        UserDbModel GetUserById(int userId);
        IEnumerable<UserDbModel> GetAllUsers();
        bool AddUser(UserDbModel user);
        bool UpdateUser(UserDbModel user);
        bool DeleteUser(int userId);
        bool UserExists(int userId);
        bool ValidateCredentials(string username, string password);
        void DeactivateUser(int userId);
    }
}
