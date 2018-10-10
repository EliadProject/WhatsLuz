﻿using System;
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

        public static Place getPlaceInfo(int id)
        {
            var db = new SqlConnectionDataContext();

            return ((from myRow in db.Places.Where(u => u.Id == id) select myRow).SingleOrDefault());
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

        public static bool updatePlaceInput(FormCollection userUpdate)
        {
            var db = new SqlConnectionDataContext();
            int placeID = int.Parse(userUpdate["placeID"]);

            var placeObj =
                        (from place in db.Places
                         where place.Id == placeID
                         select place).SingleOrDefault();

            // Execute the query, and change the column values
            // you want to change.

            placeObj.Name = userUpdate["name"];
            placeObj.Address = userUpdate["address"];
            placeObj.Description = userUpdate["description"];
            placeObj.lat = double.Parse(userUpdate["lat"]);
            placeObj.lng = double.Parse(userUpdate["lng"]);

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

        public static void removeUserByID(int userID)
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