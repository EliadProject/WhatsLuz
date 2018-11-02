

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
        public ActionResult Login(UserModel value)
        {
            string SHA256Hash = "";

            var dataContext = new SqlConnectionDataContext();

             UserAccount userAccount;

            
             //parsing json
             string DisplayName = value.displayName.ToString();
             string Email = value.email.ToString();
             string PhotoURL = value.photoURL.ToString();
             string accessToken = value.accessToken.ToString();
            
             string Userfid = value.fid.ToString();
            //retrieve user from database 
            userAccount = getUser(dataContext, Userfid);

            if (userAccount != null)
            {
                //update his details
                Console.WriteLine("Registred");
                updateUser(dataContext, userAccount, DisplayName, Email, PhotoURL,accessToken);
                SHA256Hash = userAccount.Hash;
            }
            else if (userAccount == null)
            {
                //create hash
                SHA256Hash = ManageCookie.SHA256Hash(Userfid);

                //register user
                createUser(dataContext, Userfid, DisplayName, Email, PhotoURL, SHA256Hash,accessToken);
            }
            Response.Cookies.Add(ManageCookie.CreateCookie(SHA256Hash));


            return RedirectToAction("Index","Home");


        }

        
        public ActionResult Logoff()
        {
            Response.Cookies["FacebookCookie"].Expires = DateTime.Now.AddDays(-10);
            ManageCookie.user = null;

            return RedirectToAction("LoginPage", "Home");
        }

        


        //get user by his facebook id
        static public UserAccount getUser(SqlConnectionDataContext db, string userfID)
        {
            UserAccount usera =
   (from u in db.UserAccounts
 where u.FacebookID == userfID
    select u).FirstOrDefault();
            return usera;
        }
        
 


        //update all facebook details
        static public void updateUser(SqlConnectionDataContext db, UserAccount u, string displayName, string Email, string photoURL, string accessToken)
        {
            u.DisplayName = displayName;
            u.Email = Email;
            u.PhotoURL = photoURL;
            u.LastLogon = DateTime.Now;
            u.accessToken = accessToken;
            db.SubmitChanges();
        }
        //create user from facebook
        static public void createUser(SqlConnectionDataContext db, string fid, string displayName, string Email, string photoURL,string Hash, string accessToken)
        {
            //creating instance user
            UserAccount u = new UserAccount();
            u.DisplayName = displayName;
            u.FacebookID = fid;
            u.Email = Email;
            u.PhotoURL = photoURL;
            u.isAdmin = 0;
            u.LastLogon = DateTime.Now;
            u.Hash =  Hash;
            u.accessToken = accessToken;

            //update database
            db.UserAccounts.InsertOnSubmit(u);
            db.SubmitChanges();
        }
    }
}