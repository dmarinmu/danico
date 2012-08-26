using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DanicoProject.Models;

namespace DanicoProject.Controllers
{ 
    public class HotelController : Controller
    {
        private conection db = new conection();

        //
        // GET: /Hotel/

        public ViewResult Index()
        {
            return View(db.Hotel.ToList());
        }

        //
        // GET: /Hotel/Details/5

        public ViewResult Details(long id)
        {
            Hotel hotel = db.Hotel.Single(h => h.pk_idHotel == id);
            return View(hotel);
        }

        //
        // GET: /Hotel/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Hotel/Create

        [HttpPost]
        public ActionResult Create(Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                db.Hotel.AddObject(hotel);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(hotel);
        }
        
        //
        // GET: /Hotel/Edit/5
 
        public ActionResult Edit(long id)
        {
            Hotel hotel = db.Hotel.Single(h => h.pk_idHotel == id);
            return View(hotel);
        }

        //
        // POST: /Hotel/Edit/5

        [HttpPost]
        public ActionResult Edit(Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                db.Hotel.Attach(hotel);
                db.ObjectStateManager.ChangeObjectState(hotel, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hotel);
        }

        //
        // GET: /Hotel/Delete/5
 
        public ActionResult Delete(long id)
        {
            Hotel hotel = db.Hotel.Single(h => h.pk_idHotel == id);
            return View(hotel);
        }

        //
        // POST: /Hotel/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {            
            Hotel hotel = db.Hotel.Single(h => h.pk_idHotel == id);
            db.Hotel.DeleteObject(hotel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}