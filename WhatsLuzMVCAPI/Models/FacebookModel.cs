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
        private const int FACEBOOK_COUNT_ERRORS = 10;

        public static void PostFacebook(int eventID, string title, Hashtable usersPredict)
        {
           
            //Prepare message
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Event " + eventID + " named " + title + " has been created \n");

            foreach (DictionaryEntry s in usersPredict)
            {
                String userName = SportEventModel.convertUserIDtoName(int.Parse(s.Key.ToString()));
                stringBuilder.Append("This event suites user: " + userName + " : " + s.Value + "\n");
            }

            string message = stringBuilder.ToString();

            //Retrieve access token from web config
            String accessToken = WebConfigurationManager.AppSettings["FacebookAccessToken"];


            string url = WebConfigurationManager.AppSettings["FacebookPageURL"];
            
            //configure parameters for facebook API post
            var client = new WebClient();
            var values = new NameValueCollection();
            values["message"] = message;
            values["access_token"] = accessToken;

            bool isSuccess = false;//init to not sucess for loop enter

            int countError = 0;
            //try posting until sucess
            while (!isSuccess && countError< FACEBOOK_COUNT_ERRORS)
            {
                isSuccess = true; //default state is true;
                try
                {
                    var response = client.UploadValues(url, values);
                    string responseString = Encoding.Default.GetString(response);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    isSuccess = false; //try again
                    countError++;
                }
                finally
                {
                    if (!isSuccess)
                    {
                        System.Threading.Thread.Sleep(1000);
                    }
                }
                
            }
            if(countError==FACEBOOK_COUNT_ERRORS)
                return;

        }
    }
}