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
    public class QuoteController : Controller
    {
        cHotel varHotel;
        public QuoteController()
        {
            varHotel = new cHotel();
        }

        private AllConection db = new AllConection();

        //
        // GET: /Quote/

        public ViewResult Index()
        {
            return View(db.vQuotes.ToList());
        }

        //
        // GET: /Quote/Details/5

        public ViewResult Details(string id)
        {
            vQuote vquote = db.vQuotes.Single(v => v.Uemail == id);
            return View(vquote);
        }

        //
        // GET: /Quote/Create

        public ActionResult Create(long idHParameter, string nameHParameter)
        {
            vQuote vquote = new vQuote() { QidHotel = idHParameter, Hname = nameHParameter };

            List<Models.TripType> tripTypeList = new List<Models.TripType>();
            tripTypeList = varHotel.getTripType();

            var list = new List<SelectListItem>();
            foreach (var item in tripTypeList)
            {
                list.Add(new SelectListItem() { Text = item.name, Value = item.pk_idTripType.ToString() });
            }
            ViewBag.trips = list;

            return View(vquote);
        }

        //
        // POST: /Quote/Create

        /*    public ActionResult Create(vQuote vquote)
            {
            
                return View(vquote);
            } */

        [HttpPost]
        public ActionResult Create(vQuote vquote)
        {
            
            cUser user = new cUser() { name = vquote.Uname, email = vquote.Uemail };
            long idUser = user.insertUser();

            if (vquote.QidHotel != null && idUser > -1)
            {
                DateTime currentTime = DateTime.Now;
                DateTime myTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(currentTime, "Central Standard Time");

                /*TripType tmptriptype = new TripType() { pk_idTripType  = Convert.ToInt32(vquote.idTripType) ,name = "malot"};
                UserT tmpUsert = new UserT() { pk_idUser = idUser, name = "maloU"};
                Hotel tmpHotel = new Hotel() {pk_idHotel=vquote.QidHotel, address="",description="",state= true ,fk_idTown=1};*/
                Quote quote = new Quote()
                {
                    idHotel = vquote.QidHotel,
                    iduser = idUser,
                    idTripType = vquote.idTripType,

                    description = vquote.Qdescription,
                    requestDate = myTime,
                    tripEndDate = vquote.QtripEndDate,
                    tripStartDate = vquote.QtripStartDate/*,
                    TripType = tmptriptype,
                    UserT = tmpUsert,
                    Hotel = tmpHotel*/
                };
                using (DanicoProject.Models.AllConection tmp = new Models.AllConection())
                {

                    tmp.Quotes.AddObject(quote);
                    tmp.SaveChanges();
                }
            }

            return View(vquote);
        }

        //
        // GET: /Quote/Edit/5

        public ActionResult Edit(string id)
        {

            vQuote vquote = db.vQuotes.Single(v => v.Uemail == id);
            return View(vquote);
        }

        //
        // POST: /Quote/Edit/5

        [HttpPost]
        public ActionResult Edit(vQuote vquote)
        {
            if (ModelState.IsValid)
            {
                db.vQuotes.Attach(vquote);
                db.ObjectStateManager.ChangeObjectState(vquote, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vquote);
        }

        //
        // GET: /Quote/Delete/5

        public ActionResult Delete()
        {
            return View();
        }

        //
        // POST: /Quote/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            vQuote vquote = db.vQuotes.Single(v => v.Uemail == id);
            db.vQuotes.DeleteObject(vquote);
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