

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
        // GET: Account
        [HttpPost]
        public  ActionResult Index(HttpRequestMessage value)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(HttpRequestMessage value)
        {
            /*
             var dataContext = new SqlConnectionDataContext();

             JObject json;
             UserAccount userAccount;

             //Reading the content of the request
             string val = value.Content.ReadAsStringAsync().Result;

             //converting the request to json
             json = JObject.Parse(val);

             //parsing json
             string DisplayName = json["displayName"].ToString();
             string Email = json["email"].ToString();
             string PhotoURL = json["photoURL"].ToString();
             
             //retrieve user from database 
             string Userfid = json["fid"].ToString();
             */
            // userAccount = getUser(dataContext, Userfid);

            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket
                            (
                            1, "Puki", DateTime.Now, DateTime.Now.AddMinutes(15), false, "Chooki"
                            );

             string enTicket = FormsAuthentication.Encrypt(authTicket);
             HttpCookie faCookie = new HttpCookie("Cookie1", enTicket);
             Response.Cookies.Add(faCookie);

             
            return RedirectToAction("Index","Home");


        }
        //get user by his facebook id
        /*
        static public UserAccount getUser(SqlConnectionDataContext db, string userfID)
        {
            UserAccount usera =
   (from u in db.UserAccounts
 where u.FacebookID == userfID
    select u).FirstOrDefault();
            return usera;
        }
        */

        //update all facebook details
        static public void updateUser(SqlConnectionDataContext db, UserAccount u, string displayName, string Email, string photoURL)
        {
            u.DisplayName = displayName;
            u.Email = Email;
            u.PhotoURL = photoURL;
            db.SubmitChanges();
        }
        //create user from facebook
        static public void createUser(SqlConnectionDataContext db, string fid, string displayName, string Email, string photoURL)
        {
            //creating instance user
            UserAccount u = new UserAccount();
            u.DisplayName = displayName;
            u.FacebookID = fid;
            u.Email = Email;
            u.PhotoURL = photoURL;
            u.isAdmin = 0;

            //update database
            db.UserAccounts.InsertOnSubmit(u);
            db.SubmitChanges();
        }
    }
}