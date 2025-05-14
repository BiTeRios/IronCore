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
        UserDbModel GetById(int id);
        IEnumerable<UserDbModel> GetAllUsers();
        void Update(UserDbModel u);
        void Delete(int id);
        bool UserExists(int id);
        bool ValidateCredentials(string username, string password);
        void DeactivateUser(int id);
    }
}
