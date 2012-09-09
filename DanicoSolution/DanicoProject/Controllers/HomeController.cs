using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DanicoProject.Controllers
{
    public class HomeController : Controller
    {
        
        public HomeController() {
        }

        public ActionResult Search(string search, string[] orderNumbers)
        {
         
            ViewBag.townFilter = search;
            ViewBag.checkList = orderNumbers;
          /*  string tmpcheckbox = Request.Form["orderNumbers"];
            string[] eligibility = Request.Form.GetValues("orderNumbers");*/

            List<Models.Hotel> hotelList = new System.Collections.Generic.List<Models.Hotel>();
            using (DanicoProject.Models.AllConection tmp = new Models.AllConection())
            {
                 hotelList= tmp.Hotels.Select(a =>  a).ToList();
                 if (!String.IsNullOrEmpty(search))
                 {
                     hotelList = hotelList.Where(s => s.fk_idTown.ToString().Equals(search)).ToList();
                 }
            }

            


            return View(hotelList);
        }


        public ActionResult AutocompleteAsync(string search)
        {
            List<string> townList = new System.Collections.Generic.List<string>();
            using (DanicoProject.Models.AllConection tmp = new Models.AllConection())
            {
                townList = (from a in tmp.Towns where a.name.StartsWith(search) select a.name).ToList();
            }
            return Json(townList, JsonRequestBehavior.AllowGet);
        }

        

        //
        // GET: /HomeController.cs/

      /*  public ActionResult Index()
        {
            //return View();
        }

        //
        // GET: /HomeController.cs/Details/5

        public ActionResult Details(int id)
        {
            //return View();
        }

        //
        // GET: /HomeController.cs/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /HomeController.cs/Create

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
        
        //
        // GET: /HomeController.cs/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /HomeController.cs/Edit/5

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

        //
        // GET: /HomeController.cs/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /HomeController.cs/Delete/5

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
        }*/
    }
}
