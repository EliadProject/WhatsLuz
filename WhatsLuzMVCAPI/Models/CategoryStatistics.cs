using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhatsLuzMVCAPI.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class CategoryStatistics
    {
        [JsonProperty]
        public string label { get; set; }
        [JsonProperty]
        public double value { get; set; }

    }
}