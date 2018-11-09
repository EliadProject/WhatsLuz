using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace WhatsLuzMVCAPI.Models
{
    public class SportEventModel
    {
        
        public string title{ get; set; }
        public string category { get; set; }
        public string datetime { get; set; }
        public int max_attendies { get; set; }

        public int duration { get; set; }
        public string location { get; set; }
        public string notes { get; set; }

        public static List<SportEvent_Parsed> GetEvents(FilterModel filter)
        {
            //customize filter model for quering
            filter = filterPrep(filter);

            List<SportEvent_Parsed> sportEvents;
            var dataContext = new SqlConnectionDataContext();
            sportEvents = getFilterEvents(dataContext, filter.category, filter.place, filter.maxAttendies);

            return sportEvents;
        }

        private static FilterModel filterPrep(FilterModel filtermodel)
        {
            if (filtermodel.category != null)
            {
                if (filtermodel.category.Equals("Any"))
                {
                    filtermodel.category = null;
                }
                if (filtermodel.place.Equals("Any"))
                {
                    filtermodel.place = null;
                }

            }
            return filtermodel;
        }

        private static List<SportEvent_Parsed> getFilterEvents(SqlConnectionDataContext db, string catName, string place, int maxAttendies)
        {
            List<SportEvent_Parsed> sportEvents;
            var query = (from se in db.SportEvents
                join cat in db.Categories on se.CategoryID equals cat.CategoryID
                join us in db.UserAccounts on se.OwnerID equals us.UserID
                join p in db.Places on se.PlaceID equals p.Id
                //   join p in db.Places on 
                select new SportEvent_Parsed()
                {
                    eventID = se.EventID,
                    title = se.title,
                    category = cat.Name,
                    owner = us.DisplayName,
                    max_attendies = se.MaxAttendies,
                    location = p.Name,
                    notes = se.notes,
                    startsAt = se.Date,
                    endsAt = se.Date.AddMinutes(se.Duration),
                    color = "ff0000"

                }).ToList();

            //Filter by category name
            if (!String.IsNullOrWhiteSpace(catName))
            {
                query = query.Where(p => p.category == catName).ToList();
            }
            //Filter by place name
            if (!String.IsNullOrWhiteSpace(place))
            {
                query = query.Where(p => p.location == place).ToList();
            }
            //Filter by max attendies name
            if (maxAttendies != 0)
            {
                query = query.Where(p => p.max_attendies < maxAttendies).ToList();
            }
            sportEvents = query.ToList();


            return sportEvents;

        }

        public static void createEvent(FormCollection sportEventModel)
        {
            
            var dataContext = new SqlConnectionDataContext(); 
            int placeID = getPlaceIDByName(dataContext, sportEventModel["location"]); //validate exist location
            if (placeID == 0)
                return;

            int catID = getCategoryID(dataContext, sportEventModel["category"]); //validate exist category
            if(catID ==0)
                return;

           
            SportEvent sportEvent = new SportEvent();
            int userID = ManageCookie.user.UserID;
            sportEvent.OwnerID = userID;

            sportEvent.CategoryID = catID;
            sportEvent.Date = DateTime.Parse(sportEventModel["datetime"]);
            string duration = sportEventModel["duration"];
            if (String.IsNullOrWhiteSpace(duration))
            {
                sportEvent.Duration = 120;
            }
            else
            {
                if(!ValidationModel.ValidDuration(duration)) //validate duration 
                    return;
                sportEvent.Duration = int.Parse(duration);
            }

            string max_attendies = sportEventModel["attendies"];
            if (String.IsNullOrWhiteSpace(max_attendies))
            {
                sportEvent.MaxAttendies = 12;
            }
            else
            {
                if (!ValidationModel.ValidAttendies(max_attendies)) // validate attendies 
                    return;
                sportEvent.MaxAttendies = int.Parse(max_attendies);
            }
            string title = sportEventModel["title"];
            sportEvent.PlaceID = placeID;
            if (!ValidationModel.LengthAndNotSpecialValidationMaxOnly(title))
                return;
            sportEvent.title = title;

            string notes = sportEventModel["notes"];
            if (!ValidationModel.LengthAndNotSpecialValidationMaxOnly(notes))
                return;
            sportEvent.notes = notes;


            dataContext.SportEvents.InsertOnSubmit(sportEvent);
            try
            {
                dataContext.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            //insert to user_event table

            Users_Event uevent = new Users_Event();
            uevent.EventID = sportEvent.EventID;
            uevent.UserID = sportEvent.OwnerID;

            dataContext.Users_Events.InsertOnSubmit(uevent);
            try
            {
                dataContext.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            //Retrieve ML instance
            MLModel mlModel = MLModel.GetInstance();

            //Predict classification for each user - ML
            Hashtable usersPredict = mlModel.Predict(uevent.UserID, sportEvent);

            //posting to facebook asyncly
            new Task(() => { FacebookModel.PostFacebook(sportEvent.EventID,sportEvent.title, usersPredict); }).Start();

        }

        public static String convertUserIDtoName(int userID)
        {
            var dataContext = new SqlConnectionDataContext();
            string userName = (from u in dataContext.UserAccounts
                where u.UserID == userID
                select u.DisplayName).FirstOrDefault();
            return userName;

        }

        private static int getPlaceIDByName(SqlConnectionDataContext db, string placeName)
        {
            return (from p in db.Places
                where p.Name == placeName
                select p.Id).FirstOrDefault();
        }

        private static int getCategoryID(SqlConnectionDataContext db, string catName)
        {
            return (from cat in db.Categories
                where cat.Name == catName
                select cat.CategoryID).FirstOrDefault();
        }

        public static bool cancelEventLocal(int eventID)
        {
            int userID = ManageCookie.user.UserID;
            bool result = SportEventModel.cancelEvent(eventID, userID);

            return result;
        }

        public static bool deleteEventLocal(int eventID)
        {
            int userID = ManageCookie.user.UserID;
            bool result = SportEventModel.deleteEvent(eventID);

            return result;
        }

        public static List<CategoryStatistics> getCategoriesStatistics()
        {
            var dataContext = new SqlConnectionDataContext();
            List<CategoryStatistics> seStatistics = getSportEventStatistics(dataContext);

            return seStatistics;
        }

        private static List<CategoryStatistics> getSportEventStatistics(SqlConnectionDataContext db)
        {
            int count_all = db.SportEvents.Count();

            List<CategoryStatistics> seStatistics = (from se in db.SportEvents
                join cat in db.Categories on se.CategoryID equals cat.CategoryID
                group cat by cat.Name into g
                select new CategoryStatistics
                {
                    label = g.Key,
                    value = 100.0 * g.Count() / count_all
                }).ToList();

            return seStatistics;
        }

        public static List<CategoryStatistics> getTopTenPlacesStatistics()
        {
            var dataContext = new SqlConnectionDataContext();
            List<CategoryStatistics> seStatistics = getPlacesStatistics(dataContext);

            return seStatistics;
        }

        private static List<CategoryStatistics> getPlacesStatistics(SqlConnectionDataContext db)
        {
            int count_all = db.SportEvents.Count();

            List<CategoryStatistics> seStatistics = (from se in db.SportEvents
                join places in db.Places on se.PlaceID equals places.Id
                group places.Address by places.Address into g
                select new CategoryStatistics
                {
                    label = g.Key,
                    value = 100.0 * g.Count() / count_all
                }).Take(10).ToList(); // Limit in SQL = Take in Linq to SQL.

            return seStatistics;
        }

        public static string Join(int eventid)
        {
            var dataContext = new SqlConnectionDataContext();
            UserAccount loggedAccount = ManageCookie.user;

            if (loggedAccount == null)
            {
                return "User is not logged in!";

            }
            int userID = loggedAccount.UserID;
            //check if owner
            int ownerID = getOwnerID(dataContext, eventid);
            if (ownerID == userID)
            {
                //user is the owner of event 
                return "User is the owner";
            }
            else
            {
                //check if user is among the attendies of the event
                int user_event_id = checkUserFromUsers_Event(dataContext, eventid, userID);
                if (user_event_id == 0)
                {
                    //user is not among the ettendies, he can join
                    addUserToEvent(dataContext, eventid, userID);
                    return "Success";
                }
                else
                {
                    return "User is among the attendies";
                }

            }
        }
        private static int getOwnerID(SqlConnectionDataContext db, int eventID)
        {
            int ownerID = (from se in db.SportEvents
                where se.EventID == eventID
                select se.OwnerID).FirstOrDefault();
            return ownerID;
        }

        private static void addUserToEvent(SqlConnectionDataContext db, int eventID, int userID)
        {
            Users_Event ue = new Users_Event();
            ue.EventID = eventID;
            ue.UserID = userID;
            db.Users_Events.InsertOnSubmit(ue);
            try
            {
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static int checkUserFromUsers_Event(SqlConnectionDataContext db, int eventID, int userID)
        {
            int Event_User_ID = (from ue in db.Users_Events

                where ue.EventID == eventID && ue.UserID == userID
                select ue.Event_User_ID).FirstOrDefault();
            return Event_User_ID;
        }

        public static bool cancelEvent(int eventID, int userID)
        {
            var db = new SqlConnectionDataContext();
            var result =
            (from uevent in db.Users_Events
             where uevent.EventID == eventID && uevent.UserID == userID
             select uevent).FirstOrDefault();
                if (result == null)
                return false;
            db.Users_Events.DeleteOnSubmit(result);
            try
            {
                db.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
                // Provide for exceptions.
            }
            

        }

        //deletes and return true if user is the owner
        //return false if the user is not the owner

        public static bool deleteEvent(int eventID, int userID)
        {
            var db = new SqlConnectionDataContext();
            var result =
        (from esport in db.SportEvents
         where esport.EventID == eventID && esport.OwnerID== userID
         select esport).FirstOrDefault();

            if (result == null)
                return false;

            db.SportEvents.DeleteOnSubmit(result);
            foreach (Users_Event uevent in db.Users_Events.Where(p => p.EventID == eventID))
            {
                db.Users_Events.DeleteOnSubmit(uevent);
            }
            
            try
            {
                db.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
                // Provide for exceptions.
            }
        }
        public static bool deleteEvent(int eventID)
        {
            var db = new SqlConnectionDataContext();
            var result =
                (from esport in db.SportEvents
                    where esport.EventID == eventID 
                    select esport).FirstOrDefault();

            if (result == null)
                return false;

            db.SportEvents.DeleteOnSubmit(result);
            foreach (Users_Event uevent in db.Users_Events.Where(p => p.EventID == eventID))
            {
                db.Users_Events.DeleteOnSubmit(uevent);
            }

            try
            {
                db.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
                // Provide for exceptions.
            }
        }

        public static List<SportEvent_Parsed> getAllEvents(SqlConnectionDataContext db)
        {

            List<SportEvent_Parsed> sportsEvents = (from se in db.SportEvents
                                                    join cat in db.Categories on se.CategoryID equals cat.CategoryID
                                                    join us in db.UserAccounts on se.OwnerID equals us.UserID
                                                    join p in db.Places on se.PlaceID equals p.Id
                                                    select new SportEvent_Parsed()
                                                    {
                                                        eventID = se.EventID,
                                                        title = se.title,
                                                        category = cat.Name,
                                                        owner = us.DisplayName,
                                                        max_attendies = se.MaxAttendies,
                                                        location = p.Name,
                                                        notes = se.notes,
                                                        startsAt = se.Date,
                                                        endsAt = se.Date.AddMinutes(se.Duration),
                                                        color = "ff0000"

                                                    }).ToList();

            return sportsEvents;
        }


        public static string getCategoryName(SqlConnectionDataContext db, int catID)
        {
            return (from cat in db.Categories
                    where cat.CategoryID == catID
                    select cat.Name).FirstOrDefault().ToString();
        }

        public static List<String> getUsersByEvent(SqlConnectionDataContext db, int eventID)
        {
            return (from ue in db.Users_Events
                    join ua in db.UserAccounts on ue.UserID equals ua.UserID
                    where ue.EventID == eventID
                    select ua.DisplayName).ToList();
        }

        public static string[] getCategoriesName()
        {
            var dataContext = new SqlConnectionDataContext();
            Table<Category> table_Categories = dataContext.Categories;
            //IEnumerator<SportEvent> enu_sportEvents = table_sportEvents.GetEnumerator();
            List<Category> list_Categories = table_Categories.ToList();
            string[] toString = new string[list_Categories.Count];
            for (int i = 0; i < list_Categories.Count; i++)
            {
                toString[i] = list_Categories[i].Name;
            }

            return toString;
        }
    }
    
}