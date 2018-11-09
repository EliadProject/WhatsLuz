using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WhatsLuzMVCAPI.Models;

namespace WhatsLuzMVCAPI.Controllers
{
    public class SportEventsController : Controller
    {
      
        // POST: SportEvents/getEvents
        //recives filter parameters in FilterModel object
        [HttpPost]
        public ActionResult GetEvents(FilterModel filter)
        {
            List<SportEvent_Parsed> sportEvents = SportEventModel.GetEvents(filter);

            return Json(sportEvents, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult createEvent(FormCollection sportEventModel)
        {
            SportEventModel.createEvent(sportEventModel);
            return RedirectToAction("Index", "Home");

        }

        [HttpPost]
        public ActionResult cancelEvent(int eventID)
        {
            bool result = SportEventModel.cancelEventLocal(eventID);
            return Json(result);
        }

        [HttpPost]
        public ActionResult deleteEvent(int eventID)
        {
            bool result = SportEventModel.deleteEventLocal(eventID);
            return Json(result);
        }

        public ActionResult getCategoriesStatistics()
        {
            List<CategoryStatistics> seStatistics = SportEventModel.getCategoriesStatistics();
            return Json(seStatistics, JsonRequestBehavior.AllowGet); ;
        }

        public ActionResult getTopTenPlacesStatistics()
        {
            List<CategoryStatistics> seStatistics = SportEventModel.getTopTenPlacesStatistics();
            return Json(seStatistics, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Join(int eventid)
        {
            string status = SportEventModel.Join(eventid);
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult getUsers(int eventid)
        {
            var dataContext = new SqlConnectionDataContext();
            List<String> users = SportEventModel.getUsersByEvent(dataContext, eventid);
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getCategoriesName()
        {
            string[] categoriesName = SportEventModel.getCategoriesName();
            return Json(categoriesName, JsonRequestBehavior.AllowGet); ;
        }
    }

}
