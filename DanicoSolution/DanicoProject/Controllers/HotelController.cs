using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DanicoProject.Models;
using DanicoProject.Models.Classes;

namespace DanicoProject.Controllers
{ 
    public class HotelController : Controller
    {
        cHotel varHotel;
        cGeocoding vargeocoding;
        public HotelController()
        {
              varHotel = new cHotel();
              vargeocoding = new cGeocoding();
        }

        private AllConection db = new AllConection();

        
        //http://stackoverflow.com/questions/4994203/unbelievable-duplicate-in-an-entity-framework-query
        //http://stackoverflow.com/questions/5558349/linq-to-entites-error-the-entity-or-complex-type-cannot-be-constructed-in
        // GET: /Hotel/Details/5
        public ViewResult Details(long id)
        {
            List<Models.vHotel> hotels = varHotel.gethotelView(id);
            ViewBag.vbServices = varHotel.getHotelService(hotels);
            ViewBag.vbprice = varHotel.getAveragePrice(id);
            ViewBag.vbcapacity = varHotel.getAverageCapacity(id);

            /*calculo lat y lng*/
            //no voy a guardar lat y lng en la bd por ahora porque no vale la pena. 
            //cada vez que haya una consulta de los detalles del hotel, se calcula 
            //la lat y lng de acuerdo a la dir q tenga en ese momento.
            vargeocoding.calculate(hotels.FirstOrDefault().Haddress);
            hotels.FirstOrDefault().Hlat = vargeocoding.lat;
            hotels.FirstOrDefault().Hlng= vargeocoding.lng;
            /*calculo lat y lng*/

            return View(hotels);
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