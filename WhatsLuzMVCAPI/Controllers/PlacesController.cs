using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using WhatsLuzMVCAPI.Models;

namespace WhatsLuzMVCAPI.Controllers
{
    public class PlacesController : Controller
    {
        // GET: Places/getPlacesName
        public ActionResult getPlacesName()
        {
            string[] toString = PlacesModel.getPlacesName();

            return Json(toString, JsonRequestBehavior.AllowGet);
        }

        // Post: Places/GetLocationCordinatesByName
        [HttpPost]
        public ActionResult GetLocationCordinatesByName(String name)
        {
            string[] toString = PlacesModel.GetLocationCordinatesByName(name);

            return Json(toString, JsonRequestBehavior.AllowGet);
        }

        // GET: Places
        public String getAllPlaces()
        {
            return PlacesModel.getAllPlaces();
        }

        // GET: Places
        public String getAllPlacesInfo()
        {
            return PlacesModel.getAllPlacesInfo();
        }
    }
}
