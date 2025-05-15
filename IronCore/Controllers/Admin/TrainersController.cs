using IronCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IronCore.Controllers.Admin
{
    [AdminMod(Order = 0)]
    [AdminMode(Order = 1)]
    public class TrainersController : BaseAdminController
    {
        public ActionResult Index() => View();
    }
}