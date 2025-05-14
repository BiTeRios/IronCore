using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IronCore.Areas.Admin.Controllers
{
	public class ProgramsController : BaseAdminController
    {
        public ActionResult Index() => View();
    }
}