using System.Web;
using System.Web.Mvc;

namespace IronCore.Filters
{
    public sealed class UserModAttribute : AuthorizeAttribute
    {
        public UserModAttribute() => Roles = "User";

        protected override void HandleUnauthorizedRequest(AuthorizationContext ctx)
        {
            if (ctx.HttpContext.User.Identity.IsAuthenticated)
                ctx.Result = new HttpStatusCodeResult(403);
            else
                base.HandleUnauthorizedRequest(ctx);
        }
    }
}
