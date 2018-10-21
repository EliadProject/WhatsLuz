using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace WhatsLuzMVCAPI.Models
{
    public class FacebookModel
    {
        private static readonly HttpClient client = new HttpClient();
        public static  void PostFacebook(int eventID, Hashtable usersPredict)
        {
           
            //prepare message
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Event " + eventID + " has been created \n");
            foreach (DictionaryEntry s in usersPredict)
            {
                stringBuilder.Append("This event suites user:" + s.Key + " : " + s.Value + "\n");
            }
            string message = stringBuilder.ToString() ;
            //Http request to facebook

            //Retrieve access token from web config
            String accessToken = WebConfigurationManager.AppSettings["FacebookAccessToken"];


            string url = "https://graph.facebook.com/1980183165357929/feed";
            string responseString;

            
            using (var client = new WebClient())
            {
                var values = new NameValueCollection();
                values["message"] = message;
                values["access_token"] = accessToken;

                var response = client.UploadValues(url, values);

                responseString = Encoding.Default.GetString(response);
            }


            

            /*

            var values = new Dictionary<string, string>
{
   { "message", message },
   { "access_token", accessToken }
};

            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync("https://graph.facebook.com/1980183165357929/feed", content);

            var responseString = await response.Content.ReadAsStringAsync();

            return responseString.ToString();
            */


        }
    }
}