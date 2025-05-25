using IronCore.BusinessLogic.Core;
using IronCore.BusinessLogic.DBModel;
using IronCore.BusinessLogic.Interfaces;
using IronCore.Domain.Entities.Session;
using IronCore.Domain.Entities.User.Login;
using IronCore.Domain.Entities.User.Registration;
using System.Linq;
using System;
using System.Web;
using IronCore.Domain.Entities.User;

public class SessionBL : UserApi, ISession
{
    /* ====== ЛОГИН / РЕГИСТРАЦИЯ ================================================= */

    public UserLoginResult UserLogin(UserLoginData data) => LoginAPI(data);
    public UserRegistrationResult UserRegistration(UserRegistrationData data)
                                                             => RegisterAPI(data);

    /* ====== COOKIE ============================================================== */

    public HttpCookie GenCookie(string loginCredential)
    {
        return Cookie(loginCredential);
    }
    public UserDTO GetUserByCookie(string apiCookieValue)
    {
        return UserCookie(apiCookieValue);
    }
}
