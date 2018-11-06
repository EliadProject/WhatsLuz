using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
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
            List<SportEvent_Parsed> sportEvents = SportEventModel.GetEvents(filter);

            return Json(sportEvents, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult createEvent(FormCollection sportEventModel)
        {
            SportEventModel.createEvent(sportEventModel);
            return RedirectToAction("Index", "Home");

        }

        [HttpPost]
        public ActionResult cancelEvent(int eventID)
        {
            bool result = SportEventModel.cancelEventLocal(eventID);
            return Json(result);
        }

        [HttpPost]
        public ActionResult deleteEvent(int eventID)
        {
            bool result = SportEventModel.deleteEventLocal(eventID);
            return Json(result);
        }

        public ActionResult getCategoriesStatistics()
        {
            List<CategoryStatistics> seStatistics = SportEventModel.getCategoriesStatistics();
            return Json(seStatistics, JsonRequestBehavior.AllowGet); ;
        }

        public ActionResult getTopTenPlacesStatistics()
        {
            List<CategoryStatistics> seStatistics = SportEventModel.getTopTenPlacesStatistics();
            return Json(seStatistics, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Join(int eventid)
        {
            string status = SportEventModel.Join(eventid);
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult getUsers(int eventid)
        {
            var dataContext = new SqlConnectionDataContext();
            List<String> users = getUsersByEvent(dataContext, eventid);
            return Json(users, JsonRequestBehavior.AllowGet);
        }
        
        static public List<SportEvent_Parsed> getAllEvents(SqlConnectionDataContext db)
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

        static public string getCategoryName(SqlConnectionDataContext db, int catID)
        {
            return (from cat in db.Categories
                where cat.CategoryID == catID
                select cat.Name).FirstOrDefault().ToString();
        }

        static public List<String> getUsersByEvent(SqlConnectionDataContext db, int eventID)
        {
            return (from ue in db.Users_Events
                    join ua in db.UserAccounts on ue.UserID equals ua.UserID
                    where ue.EventID == eventID
                    select ua.DisplayName).ToList();
        }

        public ActionResult getCategoriesName()
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

            return Json(toString, JsonRequestBehavior.AllowGet); ;
        }
    }

}
