using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WhatsLuzMVCAPI.Models
{
    public class PlacesModel
    {
        public static string[] getPlacesName()
        {
            var dataContext = new SqlConnectionDataContext();
            Table<Place> table_Places = dataContext.Places;
            List<Place> list_Places = table_Places.ToList();
            string[] toString = new string[list_Places.Count];
            for (int i = 0; i < list_Places.Count; i++)
            {
                toString[i] = list_Places[i].Name;
            }

            return toString;
        }

        public static string[] GetLocationCordinatesByName(String name)
        {
            var dataContext = new SqlConnectionDataContext();
            Table<Place> table_Places = dataContext.Places;
            List<Place> list_Places = table_Places.ToList();
            Place p = list_Places.Find(location => location.Name == name);
            string[] toString = { p.lat.ToString(), p.lng.ToString() };

            return toString;
        }

        public static String getAllPlaces()
        {
            var dataContext = new SqlConnectionDataContext();
            Table<Place> table_Places = dataContext.Places;
            List<Object> places = new List<Object>();
            var queryRes = from place in table_Places select place;

            foreach (var place in queryRes)
            {
                places.Add(new { place.Name, place.Id, place.CategoryID, place.Description, place.Address, place.lat, place.lng });
            }

            String result = JsonConvert.SerializeObject(places, Formatting.Indented,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

            return result;
        }

        public static String getAllPlacesInfo()
        {
            var dataContext = new SqlConnectionDataContext();
            Table<Place> table_Places = dataContext.Places;
            List<Place> list_Places = table_Places.ToList();
            String result = JsonConvert.SerializeObject(list_Places, Formatting.Indented,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

            return result;
        }

        public static bool isPlaceExists(int placeID)
        {
            var db = new SqlConnectionDataContext();
            var place = (from p in db.Places
                          where p.Id == placeID
                          select p.Id).FirstOrDefault();
            if (place != 0)
                return true;
            return false;
        }
    }
}