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
           
            /***combobox****/
            List<Models.TripType> tripTypeList = new List<Models.TripType>();
            tripTypeList = varHotel.getTripType();

            var list = new List<SelectListItem>();
            foreach (var item in tripTypeList)
            {
                list.Add(new SelectListItem() { Text = item.name, Value = item.pk_idTripType.ToString() });
            }
            ViewBag.trips = list;
            /***combobox****/
            return View(vquote);
        }

        //
        // POST: /Quote/Create
        [HttpPost]
        public ActionResult Create(vQuote vquote)
        {
            long idQuote = -1;
            string msge = "";

            UserT user = new UserT() { name = vquote.Uname, email = vquote.Uemail };
            cUser userTmp = new cUser(user);
            long idUser = userTmp.insertUser();
          
            if (vquote.QidHotel != null && idUser > -1)
            {
                DateTime currentTime = DateTime.Now;
                DateTime myTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(currentTime, "Central Standard Time");

                Quote quote = new Quote()
                {
                    idHotel = vquote.QidHotel,
                    iduser = idUser,
                    idTripType = vquote.idTripType,

                    description = vquote.Qdescription,
                    requestDate = myTime,
                    tripEndDate = vquote.QtripEndDate,
                    tripStartDate = vquote.QtripStartDate
                };
                cQuote quotetmp = new cQuote(quote);
                idQuote =quotetmp.insertQuote();
            }

            if (idQuote > -1)
            {
                msge = "La cotización se solicitó satisfactoriamente, En los siguientes días el hotel se pondrá en contacto contigo";
            }
            else { msge = "Se produjo un error, puedes intenta de nuevo mas tarde"; }

            /***combobox****/
            List<Models.TripType> tripTypeList = new List<Models.TripType>();
            tripTypeList = varHotel.getTripType();

            var list = new List<SelectListItem>();
            foreach (var item in tripTypeList)
            {
                list.Add(new SelectListItem() { Text = item.name, Value = item.pk_idTripType.ToString() });
            }
            ViewBag.trips = list;
            /***combobox****/

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