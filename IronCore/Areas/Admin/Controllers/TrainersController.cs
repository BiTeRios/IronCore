using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IronCore.Areas.Admin.Controllers
{
	public class TrainersController : BaseAdminController
    {
        public ActionResult Index() => View();
    }
}