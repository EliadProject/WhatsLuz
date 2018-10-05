using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WhatsLuzMVCAPI.Models
{
    public class AdminModel
    {
        public static List<UserAccount> getUsersList()
        {
            var db = new SqlConnectionDataContext();

            List<UserAccount> usersList = (from myRow in db.UserAccounts
                                           select myRow).ToList();

            return usersList;
        }

        public static void removeUserByEmail(int userID)
        {
            var db = new SqlConnectionDataContext();

            var user = db.UserAccounts.Where(u => u.UserID == userID).SingleOrDefault();
            if (user != null)
            {
                db.UserAccounts.DeleteOnSubmit(user);
                db.SubmitChanges();
            }
        }
        public static List<UserAccount> filterUsers(FilterUsersModel model)
        {
            //retrieving filter parameters
            string name = model.name;
            string address = model.address;
            string email = model.email;
            
            var db = new SqlConnectionDataContext();

            List<UserAccount> usersList;
            var query = (from users in db.UserAccounts
                         select users).ToList();

            //Filter by display name name
            if (!String.IsNullOrWhiteSpace(name))
            {
                query = query.Where(p => p.DisplayName.Contains(name)).ToList();
            }
            //Filter by address name
            if (!String.IsNullOrWhiteSpace(address))
            {
                query = query.Where(p => p.Address.Contains(address)).ToList();
            }
            //Filter by email
            if (!String.IsNullOrWhiteSpace(email))
            {
                query = query.Where(p => p.Email.Contains(email)).ToList();
            }
            usersList = query.ToList();

            return usersList;
        }
    }
}