using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WhatsLuzMVCAPI.Models
{
    public class FacebookModel
    {
        private static readonly HttpClient client = new HttpClient();
        public static async Task<string> PostAsync(string accessToken,Hashtable usersPredict)
        {
           
            //prepare message
            StringBuilder stringBuilder = new StringBuilder();
            foreach (DictionaryEntry s in usersPredict)
            {
                stringBuilder.Append("The event suites user: " + s.Key + " : " + s.Value);
            }
            string message = stringBuilder.ToString() ;
            //Http request to facebook
            

        var values = new Dictionary<string, string>
{
   { "message", message },
   { "access_token", accessToken }
};

            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync("https://www.facebook.com/Whats-Luz-1980183165357929", content);

            var responseString = await response.Content.ReadAsStringAsync();

            return responseString.ToString();
        }
    }
}