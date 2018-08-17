using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WhatsLuzMVCAPI.Models;
using System.Data.Linq.Mapping;
using System.Data.Linq;
using System.Security.Claims;
using Microsoft.Owin.Security.Cookies;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Web.Security;

namespace WhatsLuzMVCAPI.Controllers
{
    public class UsersController : ApiController
    {
        // GET: api/Users
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Users/5
        public string Get(int id)
        {
            return "value";
        }
        
        [HttpPost]
        // POST: Users/Post
        public void Post(HttpRequestMessage value)
        {



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
            userAccount = getUser(dataContext, Userfid);


            /*
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, Userfid));    
            var id = new ClaimsIdentity(claims,
                                        DefaultAuthenticationTypes.ApplicationCookie);

            var ctx = Request.GetOwinContext();
            var authenticationManager = ctx.Authentication;
            authenticationManager.SignIn(id);
            */
            /*
            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket
                           (
                           1, DisplayName, DateTime.Now, DateTime.Now.AddMinutes(15), false, Userfid
                           );

            string enTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie faCookie = new HttpCookie("Cookie1", enTicket);
            Response.Cookies.Add(faCookie);
            */
           // bool auth = User.Identity.IsAuthenticated;

            if (userAccount != null)
            {
                //update his details
                Console.WriteLine("Registred");
                updateUser(dataContext, userAccount, DisplayName,Email,PhotoURL);
            }
            else if (userAccount == null)
            {
                //register user
                createUser(dataContext, Userfid, DisplayName, Email, PhotoURL);
            }

            //Managing Permission Level
            /*
            if (userAccount.isAdmin == 1)
            {
                //user is admin
                
            }
            */

           
        }

        

        // PUT: api/Users/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Users/5
        public void Delete(int id)
        {
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
       static public void updateUser(SqlConnectionDataContext db, UserAccount u, string displayName, string Email, string photoURL)
        {
            u.DisplayName = displayName;
            u.Email = Email;
            u.PhotoURL = photoURL ;
            db.SubmitChanges();
        }
        //create user from facebook
        static public void createUser(SqlConnectionDataContext db,string fid, string displayName, string Email, string photoURL)
        {
            //creating instance user
            UserAccount u = new UserAccount();
            u.DisplayName = displayName;
            u.FacebookID = fid;
            u.Email = Email;
            u.PhotoURL = photoURL;
            u.isAdmin = 0 ;

            //update database
            db.UserAccounts.InsertOnSubmit(u);
            db.SubmitChanges();
        }
    }
}
