using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WhatsLuzMVCAPI.Models;

namespace WhatsLuzMVCAPI.Controllers
{
    public class AdminController : Controller
    {
    
        // GET: Admin
        public ActionResult Index()
        {
           
            if (ManageCookie.isAdmin() == false)
                return RedirectToAction("Index", "Home");
            ViewBag.Title = "Admin Page";
            return View();
        }
        
        public ActionResult Users()
        {
            if (ManageCookie.isAdmin() == false)
                return RedirectToAction("Index", "Home");
            ViewBag.Title = "Users Managments";

            //ViewBag.UserList = AdminModel.filterUsers(model);
            ViewBag.UserList = AdminModel.getUsersList();

            return View();

        }

        [HttpPost]
        public ActionResult Users(FormCollection form)
        {
            if (ManageCookie.isAdmin() == false)
                return RedirectToAction("Index", "Home");
            ViewBag.Title = "Users Managments";

            ViewBag.UserList = AdminModel.filterUsers(form);

            return View();

        }
        
        public ActionResult Places()
        {
            if (ManageCookie.isAdmin() == false)
                return RedirectToAction("Index", "Home");
            ViewBag.Title = "Places Managments";

            ViewBag.PlacesList = AdminModel.getPlacesList();

            return View();

        }

        [HttpPost]
        public ActionResult Places(FormCollection form)
        {
            if (ManageCookie.isAdmin() == false)
                return RedirectToAction("Index", "Home");
            ViewBag.Title = "Places Managments";

            ViewBag.PlacesList = AdminModel.filterPlaces(form);

            return View();

        }

        [HttpGet]
        public ActionResult EditUser(int id)
        {
            if (ManageCookie.isAdmin() == false)
                return RedirectToAction("Index", "Home");
            ViewBag.Title = "User Edit";

            //check event exist
            if (!AccountModel.isUserExists(id))
                return RedirectToAction("Users");

            ViewBag.userInfo = AdminModel.getUserInfo(id);

            return View();

        }
        [HttpGet]
        public ActionResult EditEvent(int eventID)
        {
            if (ManageCookie.isAdmin() == false)
                return RedirectToAction("Index", "Home");
            ViewBag.Title = "Event Edit";

            //check event exist
            if (!SportEventModel.isEventExists(eventID))
                return RedirectToAction("Events");

            ViewBag.eventInfo = AdminModel.getEventInfo(eventID);
            ViewBag.eventAttenides = AdminModel.getEventAttendies(eventID);
            return View();

        }
        [HttpPost]
        public ActionResult updateEvent(FormCollection eventUpdate)
        {
            if (ManageCookie.isAdmin() == false)
                return RedirectToAction("Index", "Home");

            AdminModel.updateEventInput(eventUpdate);
            return RedirectToAction("Events", "Admin");
        }
        
        [HttpGet]
        public ActionResult EditPlace(int id)
        {
            if (ManageCookie.isAdmin() == false)
                return RedirectToAction("Index", "Home");
            ViewBag.Title = "Place Edit";

            //check event exist
            if (!PlacesModel.isPlaceExists(id))
                return RedirectToAction("Places");

            ViewBag.placeInfo = AdminModel.getPlaceInfo(id);

            return View();

        }

        [HttpPost]
        public ActionResult EditUser(FormCollection userUpdate)
        {
            if (ManageCookie.isAdmin() == false)
                return RedirectToAction("Index", "Home");
            ViewBag.Title = "User Edit";

            bool updateStatus = AdminModel.updateUserInput(userUpdate);
            return RedirectToAction("Users");

        }

        [HttpPost]
        public ActionResult EditPlace(FormCollection userUpdate)
        {
            if (ManageCookie.isAdmin() == false)
                return RedirectToAction("Index", "Home");
            ViewBag.Title = "Place Edit";

            bool isUpdate = AdminModel.updatePlaceInput(userUpdate);

            return RedirectToAction("Places");

        }

        [HttpGet]
        public ActionResult DeleteEvent(int eventID)
        {

            SportEventModel.deleteEventLocal(eventID);

            return RedirectToAction("Events");
        }


        [HttpGet]
        public ActionResult Delete(int userID)
        {
            AdminModel.removeUserByID(userID);

            return RedirectToAction("Users");
        }
        [HttpGet]
        public ActionResult DeletePlace(int id)
        {
            AdminModel.removePlaceByID(id);

            return RedirectToAction("Places");
        }

        public ActionResult Statistics()
        {
            ViewBag.Title = "Statistics";
            if (ManageCookie.isAdmin() == false)
                return RedirectToAction("Index", "Home");

            return View();
        }

        public ActionResult Events()
        {
            if (ManageCookie.isAdmin() == false)
                return RedirectToAction("Index", "Home");
            ViewBag.Title = "Events Managments";

            ViewBag.EventsList = AdminModel.getEventsList();

            return View();

        }

    }
}