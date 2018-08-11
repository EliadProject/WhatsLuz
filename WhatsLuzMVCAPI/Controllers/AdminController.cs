using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WhatsLuzMVCAPI.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            ViewBag.Title = "Admin Page";

            return View();
        }

        public ActionResult Users()
        {
            ViewBag.Title = "Users Managments";

            return View();
        }

        public ActionResult Statistics()
        {
            ViewBag.Title = "Statistics";

            return View();
        }
    }
}