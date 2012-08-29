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
        private AllConection db = new AllConection();

        //
        // GET: /Hotel/

        public ViewResult Index()
        {
            return View(db.vHotels.ToList());
        }

        //
        // GET: /Hotel/Details/5

        public ViewResult Details(long id)
        {
            List<vHotel> vlist2 = new List<vHotel>();
            vlist2 = db.vHotels.Select(v => v.Hpk_idHotel == id) as List<vHotel>;
            vHotel vhotel = db.vHotels.Select(v => v.Hpk_idHotel == id).Take(1) as vHotel;
            return View(vhotel);
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
        public ActionResult Create(vHotel vhotel)
        {
            if (ModelState.IsValid)
            {
                db.vHotels.AddObject(vhotel);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(vhotel);
        }
        
        //
        // GET: /Hotel/Edit/5
 
        public ActionResult Edit(long id)
        {
            vHotel vhotel = db.vHotels.Single(v => v.Hpk_idHotel == id);
            return View(vhotel);
        }

        //
        // POST: /Hotel/Edit/5

        [HttpPost]
        public ActionResult Edit(vHotel vhotel)
        {
            if (ModelState.IsValid)
            {
                db.vHotels.Attach(vhotel);
                db.ObjectStateManager.ChangeObjectState(vhotel, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vhotel);
        }

        //
        // GET: /Hotel/Delete/5
 
        public ActionResult Delete(long id)
        {
            vHotel vhotel = db.vHotels.Single(v => v.Hpk_idHotel == id);
            return View(vhotel);
        }

        //
        // POST: /Hotel/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {            
            vHotel vhotel = db.vHotels.Single(v => v.Hpk_idHotel == id);
            db.vHotels.DeleteObject(vhotel);
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