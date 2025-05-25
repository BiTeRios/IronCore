using IronCore.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IronCore
{
    public class SessionHelper
    {
        public static UserDTO User
        {
            get => (UserDTO)HttpContext.Current.Session["User"];
            set => HttpContext.Current.Session["User"] = value;
        }
    }
}