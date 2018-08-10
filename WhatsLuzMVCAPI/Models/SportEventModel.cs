using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhatsLuzMVCAPI.Models
{
    public class SportEventModel
    {
        public string title{ get; set; }
        public string category { get; set; }
        public string datetime { get; set; }
        public int max_attendies { get; set; }

        public int duration { get; set; }
        public string location { get; set; }
        public string notes { get; set; }
    }
}