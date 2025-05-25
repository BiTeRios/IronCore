using IronCore.Domain.Enums.User;
using System.Web.Mvc;

namespace IronCore.Filters
{
    public sealed class UserOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext ctx)
        {
            if (!ctx.HttpContext.User.Identity.IsAuthenticated)
                {
                    ctx.Result = new RedirectResult("~/Auth/Login");
                }
        }
    }
}


