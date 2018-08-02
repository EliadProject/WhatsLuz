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

        // POST: api/Users
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


           if (userAccount != null)
            {
                //update his details
                Console.WriteLine("Registred");
                updateUser(dataContext, userAccount, DisplayName,Email,PhotoURL);
            }
            if (userAccount == null)
            {
                //register user
                createUser(dataContext, Userfid, DisplayName, Email, PhotoURL);
            }
          
            //Managing Permission Level

            if (userAccount.isAdmin == 1)
            {
                //user is admin
                
            }





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
