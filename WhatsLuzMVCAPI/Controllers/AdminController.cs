﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WhatsLuzMVCAPI.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Statistics()
        {
            return View();
        }
    }
}