using IronCore.Domain.Enums.User;
using System.Web.Mvc;

namespace IronCore.Filters
{
    public sealed class AdminOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext ctx)
        {
            if (!ctx.HttpContext.User.Identity.IsAuthenticated ||
                             !ctx.HttpContext.User.IsInRole("Admin"))
            {
                ctx.Result = new RedirectResult("~/Error/AccessDenied");
            }
        }
    }
}
