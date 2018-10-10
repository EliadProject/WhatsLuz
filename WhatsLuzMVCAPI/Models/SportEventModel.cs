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

        public static bool cancelEvent(int eventID, int userID)
        {
            var db = new SqlConnectionDataContext();
            var result =
        (from uevent in db.Users_Events
         where uevent.EventID == eventID && uevent.UserID == userID
         select uevent).FirstOrDefault();
            if (result == null)
                return false;
            db.Users_Events.DeleteOnSubmit(result);
            try
            {
                db.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
                // Provide for exceptions.
            }
            

        }

        //deletes and return true if user is the owner
        //return false if the user is not the owner

        public static bool deleteEvent(int eventID, int userID)
        {
            var db = new SqlConnectionDataContext();
            var result =
        (from esport in db.SportEvents
         where esport.EventID == eventID && esport.OwnerID== userID
         select esport).FirstOrDefault();

            if (result == null)
                return false;

            db.SportEvents.DeleteOnSubmit(result);
            foreach (Users_Event uevent in db.Users_Events.Where(p => p.EventID == eventID))
            {
                db.Users_Events.DeleteOnSubmit(uevent);
            }
            

                

            
            try
            {
                db.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
                // Provide for exceptions.
            }
        }
    }
    
}