using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IronCore.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AdminModeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext ctx)
        {
            var u = ctx.HttpContext.User;
            if (!u.Identity.IsAuthenticated || !u.IsInRole("Admin"))
            {
                ctx.Result = new RedirectResult("~/Account/Login"
                           + "?returnUrl=" + ctx.HttpContext.Request.RawUrl);
                return;
            }
            base.OnActionExecuting(ctx);
        }
    }

}