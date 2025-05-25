using IronCore.Domain.Entities.User;
using System.Web;
using System.Web.Mvc;

namespace IronCore.Filters          
{
    public class AdminModAttribute : AuthorizeAttribute
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var userRole = "Guest";
            if (SessionHelper.User is UserDTO UserDTO && UserDTO.Level != null)
            {
                userRole = UserDTO.Level;
            }
            else filterContext.Result = new RedirectResult("~/Error/AccessDenied");

            if (userRole != "Admin")
            {
                filterContext.Result = new RedirectResult("~/Error/AccessDenied");
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
