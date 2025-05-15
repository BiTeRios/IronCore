using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IronCore.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AdminModAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext ctx)
        {
            if (!(ctx.HttpContext.Session["IsAdminMode"] as bool? ?? false))
            {
                ctx.Result = new RedirectResult("~/Home/Index");
                return;
            }
            base.OnActionExecuting(ctx);
        }
    }

}