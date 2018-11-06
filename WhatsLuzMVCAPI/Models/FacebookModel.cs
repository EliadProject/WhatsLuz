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
       
        public static void PostFacebook(int eventID, Hashtable usersPredict)
        {
           
            //Prepare message
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


            string url = WebConfigurationManager.AppSettings["FacebookPageURL"];
            
            //configure parameters for facebook API post
            var client = new WebClient();
            var values = new NameValueCollection();
            values["message"] = message;
            values["access_token"] = accessToken;

            bool isSuccess = false;//init to not sucess for loop enter


            //try posting until sucess
            while (!isSuccess)
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
                }
                finally
                {
                    if (!isSuccess)
                    {
                        System.Threading.Thread.Sleep(1000);
                    }
                }
                
            }

           

        }
    }
}