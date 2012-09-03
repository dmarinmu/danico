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

    /*    public ViewResult Index()
        {
            return View(db.vHotels.ToList());
        }
        */
        //http://stackoverflow.com/questions/4994203/unbelievable-duplicate-in-an-entity-framework-query
        //http://stackoverflow.com/questions/5558349/linq-to-entites-error-the-entity-or-complex-type-cannot-be-constructed-in
        // GET: /Hotel/Details/5
        public ViewResult Details(long id)
        {
    //        List<vHotel> vlist2 = new List<vHotel>();
            //vlist2 = db.vHotels.Where(v => v.Hpk_idHotel == id).Where(a => a.HSidHotel ==id).ToList();
   //         vlist2 = db.vHotels.ToList();
          /*  vHotel vhotel = db.vHotels.Single(v => v.Hpk_idHotel == id);*/
   //         List<vHotel> p1 = (from v in db.vHotels
                   //  where v.viDate >= firstVisibleDate && v.viDate <= lastVisibleDate
                     //&& v.IDNo == "0330"
   //                            select v).ToList<vHotel>();//new vHotel{Hpk_idHotel = v.Hpk_idHotel, }).ToList<vHotel>();

            var query = from p in db.vHotels
                          //  where p.CategoryID == categoryID
                            select new { Hpk_idHotel = p.Hpk_idHotel,
                                        Hname = p.Hname,
                                        Haddress = p.Haddress,
                                        Hdescription = p.Hdescription,
                                        Hphone1 = p.Hphone1,
                                        Hphone2 = p.Hphone2,
                                        Hemail = p.Hemail,
                                        Hfk_idTown = p.Hfk_idTown,
                                        HimagesDirectory = p.HimagesDirectory,
                                        HcoverImage = p.HcoverImage,
                                        Hstars = p.Hstars,
                                        Hstate = p.Hstate,
                                        HSidHotel = p.HSidHotel,
                                        HSidService = p.HSidService,
                                        HSprice = p.HSprice,
                                        HSdescription = p.HSdescription,
                                        HSimagesDirectory = p.HSimagesDirectory,
                                        HScoverImage0 = p.HScoverImage0,
                                        HScoverImage1 = p.HScoverImage1,
                                        HScoverImage2 = p.HScoverImage2,
                                        HSidRoom = p.HSidRoom,
                                        Sname = p.Sname,
                                        Stype = p.Stype,
                                        Spk_idService = p.Spk_idService,
                                        RpkidHabitacion = p.RpkidHabitacion,
                                        Rtype = p.Rtype

                            };
            var products = query.ToList().Select(r => new vHotel
            {
                Hpk_idHotel = r.Hpk_idHotel,
                Hname = r.Hname,
                Haddress = r.Haddress,
                Hdescription = r.Hdescription,
                Hphone1 = r.Hphone1,
                Hphone2 = r.Hphone2,
                Hemail = r.Hemail,
                Hfk_idTown = r.Hfk_idTown,
                HimagesDirectory = r.HimagesDirectory,
                HcoverImage = r.HcoverImage,
                Hstars = r.Hstars,
                Hstate = r.Hstate,
                HSidHotel = r.HSidHotel,
                HSidService = r.HSidService,
                HSprice = r.HSprice,
                HSdescription = r.HSdescription,
                HSimagesDirectory = r.HSimagesDirectory,
                HScoverImage0 = r.HScoverImage0,
                HScoverImage1 = r.HScoverImage1,
                HScoverImage2 = r.HScoverImage2,
                HSidRoom = r.HSidRoom,
                Sname = r.Sname,
                Stype = r.Stype,
                Spk_idService = r.Spk_idService,
                RpkidHabitacion = r.RpkidHabitacion,
                Rtype = r.Rtype

            }).ToList();


            return View(products);
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