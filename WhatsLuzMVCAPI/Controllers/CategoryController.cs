using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using WhatsLuzMVCAPI.Models;

namespace WhatsLuzMVCAPI.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category/getCategoriesName
        public ActionResult getCategoriesName()
        {
            var dataContext = new SqlConnectionDataContext();
            Table<Category> table_Categories = dataContext.Categories;
            //IEnumerator<SportEvent> enu_sportEvents = table_sportEvents.GetEnumerator();
            List<Category> list_Categories = table_Categories.ToList();
            string[] toString = new string[list_Categories.Count];
            for(int i=0; i<list_Categories.Count;i++)
            {
                toString[i] = list_Categories[i].Name ;
            }
            
            return Json(toString, JsonRequestBehavior.AllowGet); ;
        }


        

        // GET: api/Category/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Category
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Category/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Category/5
        public void Delete(int id)
        {
        }
    }
}
