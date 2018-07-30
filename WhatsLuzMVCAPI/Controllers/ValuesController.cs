using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WhatsLuzMVCAPI.Models;
using System.Collections;
using System.Data.Linq;

namespace WhatsLuzMVCAPI.Controllers
{
    [Authorize]
    public class ValuesController : ApiController
    {
        
        // GET api/values
        public String Get()
        {
            return "";
            //return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value123";
        }

        // POST api/values
        public string Post([FromBody]string value)
        {
            
            var dataContext = new SqlConnectionDataContext();
            /*
            var query_eventID = (from sportEvents in dataContext.SportEvents
                                 orderby sportEvents.EventID descending
                                 select sportEvents.EventID).First();
            return query_eventID;
            */
            var events = from sportEvents in dataContext.SportEvents
                         select sportEvents;
            
            SportEvent sportEvent = new SportEvent{
                Date = DateTime.Now,
               
                Duration = 3,
                MaxAttendies = 8,
                CategoryName = "Football",
                OwnerID = "8329795" };
            
            dataContext.SportEvents.InsertOnSubmit(sportEvent);
            dataContext.SubmitChanges();
            return sportEvent.ToString();
            
                 
            /*
            int eventID;
            if (query_eventID is null)
            {
                eventID = (query_eventID);
            }
            eventID = 
            if (eventID == null)
                eventID = 1;
                          
            SportEvent sportEvent = new SportEvent();
            return "value";
            */
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
