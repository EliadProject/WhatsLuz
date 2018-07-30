using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WhatsLuzMVCAPI.Controllers
{
    public class CookieController : Controller
    {
        
        [HttpGet]
        // GET: Cookie
        public ActionResult Index()
        {
            HttpCookie cookie = new HttpCookie("userName");
            cookie.Value = Environment.UserName;

            this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);
            return RedirectToAction("Index", "Home");

            
            
        }
    }
}