using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhatsLuzMVCAPI.Models
{
    public class AccountModel
    {
        public static string LoginUser(UserModel value)
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
                updateUser(dataContext, userAccount, DisplayName, Email, PhotoURL, accessToken);
                SHA256Hash = userAccount.Hash;
            }
            else if (userAccount == null)
            {
                //create hash
                SHA256Hash = ManageCookie.SHA256Hash(Userfid);

                //register user
                createUser(dataContext, Userfid, DisplayName, Email, PhotoURL, SHA256Hash, accessToken);
            }

            return SHA256Hash;
        }

        //get user by his facebook id
        private static UserAccount getUser(SqlConnectionDataContext db, string userfID)
        {
            UserAccount usera =
                (from u in db.UserAccounts
                    where u.FacebookID == userfID
                    select u).FirstOrDefault();
            return usera;
        }
        
        //update all facebook details
        private static void updateUser(SqlConnectionDataContext db, UserAccount u, string displayName, string Email, string photoURL, string accessToken)
        {
            u.DisplayName = displayName;
            u.Email = Email;
            u.PhotoURL = photoURL;
            u.LastLogon = DateTime.Now;
            u.accessToken = accessToken;
            db.SubmitChanges();
        }

        //create user from facebook
        private static void createUser(SqlConnectionDataContext db, string fid, string displayName, string Email, string photoURL, string Hash, string accessToken)
        {
            //creating instance user
            UserAccount u = new UserAccount();
            u.DisplayName = displayName;
            u.FacebookID = fid;
            u.Email = Email;
            u.PhotoURL = photoURL;
            u.isAdmin = 0;
            u.LastLogon = DateTime.Now;
            u.Hash = Hash;
            u.accessToken = accessToken;

            //update database
            db.UserAccounts.InsertOnSubmit(u);
            db.SubmitChanges();
        }

        public static bool isUserExists(int userID)
        {
            var db = new SqlConnectionDataContext();
            var user = (from ua in db.UserAccounts
                         where ua.UserID == userID
                         select ua.UserID).FirstOrDefault();
            if (user != 0)
                return true;
            return false;
        }
    }
}