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