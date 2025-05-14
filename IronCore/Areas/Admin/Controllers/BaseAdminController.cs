using IronCore.BusinessLogic.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IronCore.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public abstract class BaseAdminController : Controller
    {
        protected readonly UserBL _userBL = new UserBL();
    }
}