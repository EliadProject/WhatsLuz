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
            if(filter.category == null)
            {
                //dont need filter
                 sportEvents = getAllEvents(dataContext);  
            }
            else
            {
                //need filter
                 sportEvents = getFilterEvents(dataContext,filter.category);  
            }
            return Json(sportEvents, JsonRequestBehavior.AllowGet);





        }

        
        [HttpPost]
        public void createEvent(SportEventModel sportEventModel)
        {

            var dataContext = new SqlConnectionDataContext();           
            SportEvent sportEvent = new SportEvent();

            sportEvent.OwnerID = Environment.UserName.ToString();

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
           
            if(sportEventModel.max_attendies == 0)
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
                sportEvent.title = "No Title" ;
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



        static public FilterModel filterPrep(FilterModel filtermodel)
        {
            if (filtermodel.category != null)
            {
                if (filtermodel.category.Equals("Any"))
                {
                    filtermodel.category = null;
                }
            }
            return filtermodel;
        }
        
        static public List<SportEvent_Parsed> getFilterEvents(SqlConnectionDataContext db, string catName)
        {
            
            List<SportEvent_Parsed> sportsEvents =  (from se in db.SportEvents
                               join cat in db.Categories on se.CategoryID equals cat.CategoryID
                               where (cat.Name == catName)
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


        static public string getCategoryName (SqlConnectionDataContext db, int catID)
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
