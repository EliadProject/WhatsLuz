using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WhatsLuzMVCAPI.Models;

namespace WhatsLuzMVCAPI.Controllers
{
    public class AdminController : Controller
    {
        private bool isAdmin;
        // GET: Admin
        public ActionResult Index()
        {
            ViewBag.Title = "Admin Page";

            if (ManageCookie.CheckCookieExists() == null)
                return RedirectToAction("LoginPage");

            isAdmin = ManageCookie.isAdmin();

            return View();
        }

        public ActionResult Users()
        {
            ViewBag.Title = "Users Managments";
            /*
            if(isAdmin == 0)
                return RedirectToAction("Home");
               */
            return View();
        }

        public ActionResult Statistics()
        {
            ViewBag.Title = "Statistics";

            return View();
        }
    }
}