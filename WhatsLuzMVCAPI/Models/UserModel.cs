using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhatsLuzMVCAPI.Models
{
    public class UserModel
    {
        public string displayName { get; set; }
        public string email { get; set; }
        public string photoURL { get; set; }
        public string fid { get; set; }
        public string accessToken { get; set; }


    }
}