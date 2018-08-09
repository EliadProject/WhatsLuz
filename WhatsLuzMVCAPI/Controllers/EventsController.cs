using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using WhatsLuzMVCAPI.Models;

namespace WhatsLuzMVCAPI.Controllers
{
    public class EventsController : ApiController
    {
        // GET: api/Events
        public SportEvent_Parsed[] Get(HttpRequestMessage value)
        {


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

            return toString;


            /*
           string val = "[{title: 'Chuki Fluki 1 ',category: \"לגרודכ\",owner: \"s6081260\",max_attendies: \"5\",location: \"םש להוא\",notes: \"רחאל אל\", startsAt: moment().startOf('week').add(3, 'days').toDate(),endsAt: moment().startOf('week').add(3, 'days').toDate() }]; ";
           JavaScriptSerializer java = new JavaScriptSerializer();
            string toreturn = java.Serialize(val);
            val = val.Replace("\"", "");
            return val;
            */



        }

        // GET: api/Events/5
        public string Get(int id)
        {
            

            return "value";
        }

        // POST: api/Events
        public string Post(HttpRequestMessage value)
        {

            var dataContext = new SqlConnectionDataContext();
            JObject json;
            SportEvent sportEvent = new SportEvent();

            //Reading the content of the request
            string val = value.Content.ReadAsStringAsync().Result;

            //converting the request to json
            json = JObject.Parse(val);

            sportEvent.OwnerID = Environment.UserName.ToString();
            sportEvent.CategoryName = json["categories"].ToString();
            sportEvent.Date = DateTime.Parse(json["datetime"].ToString());

            string duration = json["duration"].ToString();
            if (duration == "")
                sportEvent.Duration = 2;

            else
                sportEvent.Duration = int.Parse(duration);

            string attendies = json["attendies"].ToString();
            if (attendies == "")
                sportEvent.MaxAttendies = 50;
            else
                sportEvent.MaxAttendies = int.Parse(attendies);

            sportEvent.location = json["location"].ToString();
            sportEvent.notes = json["notes"].ToString();
            if (sportEvent.title == "")
                sportEvent.title = "ללא כותרת";
            else
                sportEvent.title = json["title"].ToString();
           
            dataContext.SportEvents.InsertOnSubmit(sportEvent);
            try
            {
                dataContext.SubmitChanges();
            }
            catch (Exception e)
            {
                dataContext.SubmitChanges();
            }

            return val;
        }

        // PUT: api/Events/5
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE: api/Events/5
        public void Delete(int id)
        {
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
            string toreturn = java.Serialize(sportEvent_parsed).Replace(@"\", "");
            return sportEvent_parsed;


        }
    }
}
