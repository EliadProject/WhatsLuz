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
    
        // GET: Admin
        public ActionResult Index()
        {
           
            if (ManageCookie.isAdmin() == false)
                return RedirectToAction("Index", "Home");
            ViewBag.Title = "Admin Page";
            return View();
        }

        public ActionResult Users()
        {
            if (ManageCookie.isAdmin() == false)
                return RedirectToAction("Index", "Home");
            ViewBag.Title = "Users Managments";

            ViewBag.UserList = AdminModel.getUsersList();

            return View();

        }
        [HttpGet]
        public ActionResult Delete(int userID)
        {
            AdminModel.removeUserByEmail(userID);

            return RedirectToAction("Users");
        }

        public ActionResult Statistics()
        {
            ViewBag.Title = "Statistics";
            if (ManageCookie.isAdmin() == false)
                return RedirectToAction("Index", "Home");

            return View();
        }
    }
}