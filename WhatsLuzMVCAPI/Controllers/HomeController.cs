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

            return View();
        }
        public ActionResult About()
        {
            ViewBag.Title = "About the Team";
            if (ManageCookie.CheckCookieExists() == null)
                return RedirectToAction("LoginPage");

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            if (ManageCookie.CheckCookieExists() == null)
                return RedirectToAction("LoginPage");
            return View();
        }
        public ActionResult LoginPage()
        {
            ViewBag.Title = "Login Page";
            if (ManageCookie.CheckCookieExists() != null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Video()
        {
            ViewBag.Title = "Login Page";
            if (ManageCookie.CheckCookieExists() == null)
                return RedirectToAction("LoginPage");
            return View();
        }
    }
}
