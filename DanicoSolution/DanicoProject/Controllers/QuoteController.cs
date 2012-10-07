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

        public ViewResult Details()
        {

            return View();
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
                idQuote = quotetmp.insertQuote();
            }

            if (idQuote > -1)
            {
                msge = "La cotización se solicitó satisfactoriamente, En los siguientes días el hotel se pondrá en contacto contigo";
            }
            else { msge = "Se produjo un error, puedes intentar de nuevo mas tarde"; }
            ViewBag.msge = msge;

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

        public ActionResult Edit() { 
            return View();
        }

        public JsonResult AutocompleteSuggestions(string term)
        {
         /*   List<string> townList = new System.Collections.Generic.List<string>();
            using (DanicoProject.Models.AllConection tmp = new Models.AllConection())
            {
                var suggestions = from s in tmp.Towns select s.name;
                townList = suggestions.Where(n => n.ToLower().StartsWith(term.ToLower())).ToList();

            }*/

            AllConection db = new AllConection();
            var suggestions = from s in db.Towns select s.name;
            var namelist = suggestions.Where(n => n.ToLower().StartsWith(term.ToLower())).ToList();
            // return namelist.ToList();
            return Json(namelist, JsonRequestBehavior.AllowGet);

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