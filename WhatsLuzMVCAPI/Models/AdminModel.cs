using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public static List<SportEvent> getEventsList()
        {
            var db = new SqlConnectionDataContext();

            List<SportEvent> eventsList = (from myRow in db.SportEvents
                select myRow).ToList();

            return eventsList;
        }

        public static UserAccount getUserInfo(int id)
        {
            var db = new SqlConnectionDataContext();

            return ((from myRow in db.UserAccounts.Where(u => u.UserID == id) select myRow).SingleOrDefault());
        }

        public static List<Users_Event> getEventAttendies(int eventID)
        {
            var db = new SqlConnectionDataContext();

            return ((from myRow in db.Users_Events.Where(ue => ue.EventID == eventID) select myRow).ToList());
        }

        public static void updateEventInput(FormCollection eventUpdate)
        {
            // Valid event input
            if (!ValidationModel.LengthAndNotSpecialValidation(eventUpdate["title"]) ||
                !ValidationModel.isDateTime(eventUpdate["date"]) ||
                !ValidationModel.ValidAttendies(eventUpdate["attendies"]) ||
                !ValidationModel.ValidDuration(eventUpdate["duration"]) ||
            !ValidationModel.isInt(eventUpdate["notes"]))
            {
                return ;
            }
            
            var db = new SqlConnectionDataContext();
            int eventID = int.Parse(eventUpdate["eventID"]);

            var eventObj =
                        (from e in db.SportEvents
                         where e.EventID == eventID
                         select e).SingleOrDefault();

            // Execute the query, and change the column values
            // you want to change.

            eventObj.title = eventUpdate["title"];
            eventObj.Date = DateTime.Parse(eventUpdate["date"]);
            eventObj.MaxAttendies = int.Parse(eventUpdate["attendies"]);
            eventObj.Duration = int.Parse(eventUpdate["duration"]);
            eventObj.notes = eventUpdate["notes"];

            try
            {
                db.SubmitChanges();              
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
             
                // Provide for exceptions.
            }
        }
    

        public static SportEvent getEventInfo(int eventID)
        {
            var db = new SqlConnectionDataContext();

            return ((from myRow in db.SportEvents.Where(se => se.EventID == eventID) select myRow).SingleOrDefault());
        }


        public static Place getPlaceInfo(int id)
        {
            var db = new SqlConnectionDataContext();

            return ((from myRow in db.Places.Where(u => u.Id == id) select myRow).SingleOrDefault());
        }

        public static bool updateUserInput(FormCollection userUpdate)
        {
            // Valid user input
            if(!ValidationModel.EmailValidation(userUpdate["email"]) ||
                !ValidationModel.LengthAndNotSpecialValidation(userUpdate["name"]) ||
                !ValidationModel.LengthAndNotSpecialValidation(userUpdate["address"]) ||
                !ValidationModel.isInt(userUpdate["admin"]))
            {
                return false;
            }

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
                Debug.WriteLine(e.Message);
                return false;
                // Provide for exceptions.
            }

            return true;
        }

        public static bool updatePlaceInput(FormCollection placeUpdate)
        {
            // Valid place input
            if (!ValidationModel.LengthAndNotSpecialValidation(placeUpdate["name"]) ||
                !ValidationModel.LengthAndNotSpecialValidation(placeUpdate["address"]) ||
                !ValidationModel.LengthAndNotSpecialValidation(placeUpdate["description"]) ||
                !ValidationModel.isDouble(placeUpdate["lat"]) ||
                !ValidationModel.isDouble(placeUpdate["lng"]))
            {
                return false;
            }

            var db = new SqlConnectionDataContext();
            int placeID = int.Parse(placeUpdate["placeID"]);

            var placeObj =
                        (from place in db.Places
                         where place.Id == placeID
                         select place).SingleOrDefault();

            // Execute the query, and change the column values
            // you want to change.

            placeObj.Name = placeUpdate["name"];
            placeObj.Address = placeUpdate["address"];
            placeObj.Description = placeUpdate["description"];
            placeObj.lat = double.Parse(placeUpdate["lat"]);
            placeObj.lng = double.Parse(placeUpdate["lng"]);

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

        public static void removePlaceByID(int placeID)
        {
            var db = new SqlConnectionDataContext();

            var place = db.Places.Where(p => p.Id == placeID).SingleOrDefault();
            if (place != null)
            {
                db.Places.DeleteOnSubmit(place);
                db.SubmitChanges();
            }
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
        public static List<UserAccount> filterUsers(FormCollection form)
        {
            //retrieving filter parameters
            string name = form[0];
            string address = form[1];
            string email = form[2];

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

        public static List<Place> filterPlaces(FormCollection form)
        {
            //retrieving filter parameters
            string name = form[0];
            string address = form[1];

            var db = new SqlConnectionDataContext();

            List<Place> placesList;
            var query = (from place in db.Places
                         select place).ToList();

            //Filter by display name name
            if (!String.IsNullOrWhiteSpace(name))
            {
                query = query.Where(p => p.Name.Contains(name)).ToList();
            }
            //Filter by address name
            if (!String.IsNullOrWhiteSpace(address))
            {
                query = query.Where(p => p.Address.Contains(address)).ToList();
            }
            placesList = query.ToList();

            return placesList;
        }
    }
}