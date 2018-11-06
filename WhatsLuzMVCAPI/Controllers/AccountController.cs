

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WhatsLuzMVCAPI.Models;

namespace WhatsLuzMVCAPI.Controllers
{
    public class AccountController : Controller
    {
        [HttpPost]
        public ActionResult Login(UserModel value)
{
            string SHA256Hash = AccountModel.LoginUser(value);
            Response.Cookies.Add(ManageCookie.CreateCookie(SHA256Hash));

            return RedirectToAction("Index","Home");
        }

        
        public ActionResult Logoff()
        {
            Response.Cookies["FacebookCookie"].Expires = DateTime.Now.AddDays(-10);
            ManageCookie.user = null;

            return RedirectToAction("LoginPage", "Home");
        }
    }
}