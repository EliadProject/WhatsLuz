using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WhatsLuzMVCAPI.Models;

namespace WhatsLuzMVCAPI.Controllers
{
    public class PlacesController : Controller
    {
        // GET: Places/getPlacesName
        public ActionResult getPlacesName()
        {
            var dataContext = new SqlConnectionDataContext();
            Table<Place> table_Places = dataContext.Places;
            List<Place> list_Places = table_Places.ToList();
            string[] toString = new string[list_Places.Count];
            for (int i = 0; i < list_Places.Count; i++)
            {
                toString[i] = list_Places[i].Name;
            }

            return Json(toString, JsonRequestBehavior.AllowGet); ;
        }

        // GET: Places/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Places/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Places/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Places/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Places/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Places/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Places/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
