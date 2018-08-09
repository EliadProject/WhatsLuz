using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhatsLuzMVCAPI.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class SportEvent_Parsed
    {
        /*
        private string title;
        private string category;
        private string owner;
        private int max_attendies;
        private string notes;
        private DateTime startAt;
        private DateTime endsAt;
        */

        /*
    public SportEvent_Parsed(SportEvent sportEvent)
    {

        this.title = "random title";
        this.category = sportEvent.CategoryName;
        this.owner = sportEvent.OwnerID.ToString();
        this.max_attendies = sportEvent.MaxAttendies;
        this.notes = sportEvent.notes;
        this.startAt = sportEvent.Date.AddHours(sportEvent.StartHour);
        this.endsAt = this.startAt.AddMinutes(sportEvent.Duration);
       


    }
     */
        [JsonProperty]
        public string title { get; set; }

        [JsonProperty]
        public string category { get; set; }

        [JsonProperty]
        public string owner { get; set; }

        [JsonProperty]
        public int max_attendies { get; set; }

        [JsonProperty]
        public string location { get; set; }

        [JsonProperty]
        public string notes { get; set; }

        [JsonProperty]
        public DateTime startsAt { get; set; }

        [JsonProperty]
        public DateTime endsAt { get; set; }

        [JsonProperty]
        public string color { get; set; }
        



    }
}