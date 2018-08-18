using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WhatsLuzMVCAPI.Models;

namespace WhatsLuzMVCAPI.Controllers
{
    public class SportEventsController : Controller
    {


        // POST: SportEvents/getEvents
        //recives filter parameters in FilterModel object
        [HttpPost]
        public ActionResult GetEvents(FilterModel filter)
        {


            //customize filter model for quering
            filter = filterPrep(filter);

            List<SportEvent_Parsed> sportEvents;
            var dataContext = new SqlConnectionDataContext();
            if (filter.category == null)
            {
                //dont need filter
                sportEvents = getAllEvents(dataContext);
            }
            else
            {
                //need filter
                sportEvents = getFilterEvents(dataContext, filter.category, filter.place);
            }
            return Json(sportEvents, JsonRequestBehavior.AllowGet);





        }


        [HttpPost]
        public void createEvent(SportEventModel sportEventModel)
        {

            var dataContext = new SqlConnectionDataContext();
            SportEvent sportEvent = new SportEvent();

            sportEvent.OwnerID = ManageCookie.user.UserID;

            sportEvent.CategoryID = getCategoryID(dataContext, sportEventModel.category);
            sportEvent.Date = DateTime.Parse(sportEventModel.datetime);
            if (sportEventModel.duration == 0)
            {
                sportEvent.Duration = 120;
            }
            else
            {
                sportEvent.Duration = sportEventModel.duration;
            }

            if (sportEventModel.max_attendies == 0)
            {
                sportEvent.MaxAttendies = 12;
            }
            else
            {
                sportEvent.MaxAttendies = sportEventModel.max_attendies;
            }

            sportEvent.location = sportEventModel.location;
            sportEvent.notes = sportEventModel.notes;

            if (sportEventModel.title == null)
            {
                sportEvent.title = "No Title";
            }
            else
            {
                sportEvent.title = sportEventModel.title;
            }

            dataContext.SportEvents.InsertOnSubmit(sportEvent);
            try
            {
                dataContext.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


        }

        public ActionResult getCategoriesStatistics()
        {
            var dataContext = new SqlConnectionDataContext();
            List<CategoryStatistics> seStatistics = getSportEventStatistics(dataContext);
            return Json(seStatistics, JsonRequestBehavior.AllowGet); ;
        }
        [HttpPost]
        public String Join(int eventid)
        {
            var dataContext = new SqlConnectionDataContext();

            UserAccount loggedAccount = ManageCookie.user;
            int userID = loggedAccount.UserID;
            if (loggedAccount == null)
            {
                return "User is not logged in!";
            }
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
                    //join user
                    return "Success";
                }
                else
                {
                    return "User is among the attendies";
                }
                    
                    
            }



        }


        static public FilterModel filterPrep(FilterModel filtermodel)
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


        static public int getOwnerID(SqlConnectionDataContext db, int eventID)
        {
            int ownerID = (from se in db.SportEvents
                           where se.EventID == eventID
                           select se.OwnerID).FirstOrDefault();
            return ownerID;
        }
        static public int checkUserFromUsers_Event(SqlConnectionDataContext db, int eventID, int userID)
        {
            int Event_User_ID = (from ue in db.Users_Events
                                 where ue.EventID == eventID && ue.UserID == userID
                                 select ue.Event_User_ID).FirstOrDefault();
            return Event_User_ID;
        }
        static public List<SportEvent_Parsed> getFilterEvents(SqlConnectionDataContext db, string catName, string place)
        {
            string whereClause = "";
            List<SportEvent_Parsed> sportEvents;
            var query = (from se in db.SportEvents
                         join cat in db.Categories on se.CategoryID equals cat.CategoryID
                         select new SportEvent_Parsed()
                         {
                             title = se.title,
                             category = cat.Name,
                             owner = se.OwnerID.ToString(),
                             max_attendies = se.MaxAttendies,
                             location = se.location,
                             notes = se.notes,
                             startsAt = se.Date,
                             endsAt = se.Date.AddMinutes(se.Duration),
                             color = "ff0000"

                         }).ToList();

            if (!String.IsNullOrWhiteSpace(catName))
            {
                //whereClause+= "category == " + catName;
                sportEvents = query.Where(p => p.category == catName).ToList();

                //sportEvents = query.AsQueryable().w .Where("category == @0",  catName);
            }
            else
            {
                sportEvents = query.ToList();
            }

            return sportEvents;




        }



        static public List<SportEvent_Parsed> getAllEvents(SqlConnectionDataContext db)
        {

            List<SportEvent_Parsed> sportsEvents = (from se in db.SportEvents
                                                    join cat in db.Categories on se.CategoryID equals cat.CategoryID
                                                    select new SportEvent_Parsed()
                                                    {
                                                        title = se.title,
                                                        category = cat.Name,
                                                        owner = se.OwnerID.ToString(),
                                                        max_attendies = se.MaxAttendies,
                                                        location = se.location,
                                                        notes = se.notes,
                                                        startsAt = se.Date,
                                                        endsAt = se.Date.AddMinutes(se.Duration),
                                                        color = "ff0000"

                                                    }).ToList();

            return sportsEvents;
        }

        static public List<CategoryStatistics> getSportEventStatistics(SqlConnectionDataContext db)
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


        static public string getCategoryName(SqlConnectionDataContext db, int catID)
        {
            return (from cat in db.Categories
                    where cat.CategoryID == catID
                    select cat.Name).FirstOrDefault().ToString();
        }
        static public int getCategoryID(SqlConnectionDataContext db, string catName)
        {
            return (from cat in db.Categories
                    where cat.Name == catName
                    select cat.CategoryID).FirstOrDefault();
        }
    }

}
