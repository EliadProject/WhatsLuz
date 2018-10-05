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

        public static List<Place> getPlacesList()
        {
            var db = new SqlConnectionDataContext();

            List<Place> placesList = (from myRow in db.Places
                                           select myRow).ToList();

            return placesList;
        }

        public static UserAccount getUserInfo(int id)
        {
            var db = new SqlConnectionDataContext();

            return ((from myRow in db.UserAccounts.Where(u => u.UserID == id) select myRow).SingleOrDefault());
        }

        public static bool updateUserInput(FormCollection userUpdate)
        {
            var db = new SqlConnectionDataContext();
            int userID = int.Parse(userUpdate["userID"]);

            var userObj =
                        (from user in db.UserAccounts
                         where user.UserID == userID
                         select user).SingleOrDefault();

            // Execute the query, and change the column values
            // you want to change.
          
            userObj.DisplayName = userUpdate["name"];
            userObj.Address = userUpdate["address"];
            userObj.Email = userUpdate["email"];
            userObj.isAdmin = Byte.Parse(userUpdate["admin"]);
            
            // Insert any additional changes to column values.

            // Submit the changes to the database.
            try
            {
                db.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
                // Provide for exceptions.
            }

            return true;
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
    }
}