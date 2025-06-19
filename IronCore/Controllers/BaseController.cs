using IronCore.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IronCore.Controllers
{
    public class BaseController : Controller
    {
        protected ICart _cart;

        public BaseController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _cart = bl.GetCartBL();
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            var cart = _cart.GetCurrentCart();
            ViewBag.CartCount = cart?.Products?.Count ?? 0;
            ViewBag.CartTotal = cart?.Price ?? 0m;
        }
    }

}