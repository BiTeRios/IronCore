using IronCore.Domain.Entities.User;
using IronCore.Domain.Entities.User.Login;
using IronCore.Domain.Entities.User.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace IronCore.BusinessLogic.Interfaces
{
    public interface ISession
    {
        UserLoginResult UserLogin(UserLoginData data);
        UserRegistrationResult UserRegistration(UserRegistrationData data);
        void SaveLastLogin(string userName, DateTime whenUtc, string ip);
    }
}
