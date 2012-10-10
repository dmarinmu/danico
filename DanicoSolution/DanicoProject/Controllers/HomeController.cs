using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DanicoProject.Models.Classes;
using DanicoProject.Models;

namespace DanicoProject.Controllers
{
    public class HomeController : Controller
    {
        cHotel varHotel;
        public HomeController() {
              varHotel = new cHotel();
        }

        public ActionResult Search(string search, string[] orderNumbers)
        {
         
            ViewBag.townFilter = search;
            ViewBag.checkList = orderNumbers;
            List<long?> serviceIdInDB = varHotel.getServiceID(orderNumbers);
            List<Models.Hotel> hotelList = new List<Models.Hotel>();

            using (AllConection tmp = new AllConection())
            {
                
                 //1s filter
                if (!String.IsNullOrEmpty(search))
                 {
                     hotelList = tmp.Hotels.Select(a => a).ToList();
                     long idTown = varHotel.getIdTown(search);
                     //2d filter 
                     if (idTown > -1)
                     {
                         hotelList = hotelList.Where(s => s.fk_idTown.Equals(idTown)).ToList();
                         //3d filter
                         if (serviceIdInDB.Count > 0)
                         {
                             List<long> hotelListIDs = hotelList.ConvertAll(obj => obj.pk_idHotel).ToList();

                             hotelList =
                              (from hotel in tmp.Hotels
                               join service in tmp.HotelServices on hotel.pk_idHotel equals service.idHotel
                               where hotelListIDs.Contains(hotel.pk_idHotel)
                               where serviceIdInDB.Contains(service.idService)
                               select hotel).Distinct().ToList<Models.Hotel>();
                         }
                     }
                    
                 }
            }
            return View(hotelList);
        }

        [ChildActionOnly]
        public ActionResult getdiscount()
        {
            List<Models.vDiscount> disList = new List<Models.vDiscount>();
            disList = varHotel.getDiscountView();
            return PartialView("_PartialFooter", disList);
        }

        public ActionResult AutocompleteAsync(string term)
        {
            List<string> townList = new System.Collections.Generic.List<string>();
            using (AllConection tmp = new AllConection())
            {
                townList = (from a in tmp.Towns where a.name.StartsWith(term) select a.name).Take(10).ToList();
            }

            var ret = townList;
            return Json(ret, JsonRequestBehavior.AllowGet);
        }


        public JsonResult AutocompleteSuggestions(string searchText)
        {

            var tmp = varHotel.getTowns(searchText);
            return Json(tmp, JsonRequestBehavior.AllowGet);

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
