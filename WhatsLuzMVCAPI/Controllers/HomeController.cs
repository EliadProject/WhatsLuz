using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WhatsLuzMVCAPI.Models;

namespace WhatsLuzMVCAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "WhatsLuz";

            if (ManageCookie.CheckCookieExists() == null)
                return RedirectToAction("LoginPage");

            ViewBag.isAdmin = ManageCookie.isAdmin();

            return View();
        }
        public ActionResult About()
        {
            ViewBag.Title = "About the Team";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult LoginPage()
        {
            ViewBag.Title = "Login Page";

            return View();
        }
    }
}
