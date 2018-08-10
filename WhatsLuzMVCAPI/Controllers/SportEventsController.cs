using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
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

            var dataContext = new SqlConnectionDataContext();
            Table<SportEvent> table_sportEvents = dataContext.SportEvents;
            //IEnumerator<SportEvent> enu_sportEvents = table_sportEvents.GetEnumerator();
            List<SportEvent> list_sportEvents = table_sportEvents.ToList();


            SportEvent_Parsed[] toString = new SportEvent_Parsed[list_sportEvents.Count];
            for (int i = 0; i < list_sportEvents.Count; i++)
            {
                //string json = JsonConvert.SerializeObject(list_sportEvents[i]);
                toString[i] = parsedSportEvent(list_sportEvents[i]);

            }
           
            
            return Json(toString, JsonRequestBehavior.AllowGet);


         

        }

        // GET: SportEvents/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SportEvents/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: SportEvents/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SportEvents/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        static public SportEvent_Parsed parsedSportEvent(SportEvent sportEvent)
        {
            //SportEvent_Parsed parsed = new SportEvent_Parsed(sportEvent);

            SportEvent_Parsed sportEvent_parsed = new SportEvent_Parsed()
            {
                title = sportEvent.title,
                category = sportEvent.CategoryName,
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
                    filtermodel.category = "";
                }
            }
            return filtermodel;
        }
        /*
        static public UserAccount getFilterEvents(SqlConnectionDataContext db, string categoryName)
        {
            SportEvent sevent =
            (from se in db.SportEvents
                join cat in db.Categories


                select u).FirstOrDefault();
            return usera;
        }
        */
    }
   
}
