﻿using System;
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
        [HttpPost]
        public ActionResult GetEvents(FilterModel filter)
        {

            //Reading the content of the request
            //string val = value.Content.ReadAsStringAsync().Result;


            filter = filterPrep(filter);
            List<SportEvent> list_sportEvents;
            var dataContext = new SqlConnectionDataContext();
            if(filter.category == null)
            {
                //dont need filter
                 list_sportEvents = getAllEvents(dataContext);
            }
            else
            {
                //need filter
                 list_sportEvents = getFilterEvents(dataContext,getCategoryID(dataContext,filter.category));
            }

            SportEvent_Parsed[] toString = new SportEvent_Parsed[list_sportEvents.Count];
            for (int i = 0; i < list_sportEvents.Count; i++)
            {
                //string json = JsonConvert.SerializeObject(list_sportEvents[i]);
                toString[i] = parsedSportEvent(dataContext,list_sportEvents[i]);

            }
           
            
            return Json(toString, JsonRequestBehavior.AllowGet);

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

        static public SportEvent_Parsed parsedSportEvent(SqlConnectionDataContext dataContext, SportEvent sportEvent)
        {
            //SportEvent_Parsed parsed = new SportEvent_Parsed(sportEvent);

            SportEvent_Parsed sportEvent_parsed = new SportEvent_Parsed()
            {
                title = sportEvent.title,
                category = getCategoryName(dataContext,sportEvent.CategoryID),
                owner = sportEvent.OwnerID.ToString(),
                max_attendies = sportEvent.MaxAttendies,
                location = sportEvent.location,
                notes = sportEvent.notes,
                startsAt = sportEvent.Date,
                endsAt = sportEvent.Date.AddMinutes(sportEvent.Duration),
                color = "#ff0000"
            };

            //string toreturn = (JsonConvert.SerializeObject(sportEvent_parsed)).Replace(@"\","");
            JavaScriptSerializer java = new JavaScriptSerializer();
            string toreturn = JsonConvert.SerializeObject(sportEvent_parsed, Formatting.Indented);
            //string toreturn = java.Serialize(sportEvent_parsed).Replace(@"\", "");
            return sportEvent_parsed;


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
        
        static public List<SportEvent> getFilterEvents(SqlConnectionDataContext db, int catID)
        {
            List<SportEvent> list_sportEvents =
            (from se in db.SportEvents
             where se.CategoryID == catID
             select se).ToList();
            return list_sportEvents;


        }
        static public List<SportEvent> getAllEvents(SqlConnectionDataContext db)
        {
            Table<SportEvent> table_sportEvents = db.SportEvents;
            //IEnumerator<SportEvent> enu_sportEvents = table_sportEvents.GetEnumerator();
            List<SportEvent> list_sportEvents = table_sportEvents.ToList();
            return list_sportEvents;
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