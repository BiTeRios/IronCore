﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IronCore.Controllers
{
    public class ErrorController : BaseController
    {
        public ActionResult Page404()
        {
            return View();
        }
    }
}