using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DanicoProject.Models.Classes
{
    public class cHotel
    {
        public cHotel() { }

        public List<long> getServiceID(string[] orderNumbers)
        {
            List<long> serviceIdInDB = new List<long>();
            long serviceTmp;
            string mapService;
            if (orderNumbers != null)
            {
                using (DanicoProject.Models.AllConection tmp = new Models.AllConection())
                {
                    foreach (var item in orderNumbers)
                    {
                        mapService = this.mapService(item);
                       
                        serviceTmp = Convert.ToInt64((from a in tmp.Services where a.name.Equals(mapService) select a.pk_idService).Single());
                       
                       serviceIdInDB.Add(serviceTmp);
                    }
                }
            }
            return serviceIdInDB;
        }

        private string mapService(string serviceIdInView)
        {
            switch (serviceIdInView)
            {
                case "1":
                    return "comida";
                case "2":
                    return "parqueadero";
                case "3":
                    return "bano";
                case "4":
                    return "internet";
                case "5":
                    return "cajaFuerte";
                case "6":
                    return "lavanderia";
                case "7":
                    return "tarjetasCredito";
                default:
                    return "";
            }
        }

        public string getHotelService(List<Models.vHotel> hotelView) {
            string vbServices = "";
            foreach (var item in hotelView)
            {
                if (!String.IsNullOrEmpty(item.Sname))
                {
                    vbServices = vbServices + item.Sname + ", ";
                }
            }
            vbServices = vbServices.TrimEnd(' ');
            vbServices = vbServices.TrimEnd(',');
            return vbServices;
        }

        public List<Models.vHotel> gethotelView(long id)
        {
            List<Models.vHotel> ret = new List<vHotel>();
            using (DanicoProject.Models.AllConection db = new Models.AllConection())
            {
                #region Query Hotel Details
                var query = from p in db.vHotels
                            where p.Hpk_idHotel == id
                            select new
                            {
                                Hpk_idHotel = p.Hpk_idHotel,
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
                ret = products;
                #endregion
            }
            return ret;
        }

        public string getAveragePrice(long id) {
            string ret = "";
            using (DanicoProject.Models.AllConection db = new Models.AllConection())
            {
                var prices = db.HotelServices.Where(a => a.idHotel == id).Select(a => a.price).ToList() ; //(from a in db.HotelServices where a.idHotel.Equals(id) select a.price).ToList();
                if (prices.Count>=0)
                {
                  double? min = prices.Min();
                  double? max = prices.Max();
                  if (min == max)
                  {
                      ret = min.ToString();
                  }
                  else
                  {
                      ret = min.ToString() + "-" + max.ToString();
                  }
                }if (ret == "-"){ret ="";}   
            }
            return ret;
        }

        public string getAverageCapacity(long id)
        {
            string ret = "";
            using (DanicoProject.Models.AllConection db = new Models.AllConection())
            {
                var capacities = db.HotelServices.Where(a => a.idHotel == id).Select(a => a.capactity).ToList(); //(from a in db.HotelServices where a.idHotel.Equals(id) select a.price).ToList();
                if (capacities.Count >= 0)
                {
                    double? min = capacities.Min();
                    double? max = capacities.Max();
                    if (min == max)
                    {
                        ret = min.ToString();
                    }
                    else {
                        ret = min.ToString() + "-" + max.ToString();
                    }
                    
                } if (ret == "-") { ret = ""; }
            }
            return ret;
        }

        public List<Models.vDiscount> getDiscountView()
        {
            DateTime currentTime = DateTime.Now; //new DateTime(1990, 12, 02, 19, 31, 30, DateTimeKind.Utc);

            DateTime myTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(currentTime, "Central Standard Time");

            List<Models.vDiscount>  ret = new List<vDiscount>();
            using (DanicoProject.Models.AllConection db = new Models.AllConection())
            {
                #region Query Hotel vDiscounts
                var query = from p in db.vDiscounts
                         //   where p.Hpk_idHotel == id
                            select new
                            {
                                Hpk_idHotel = p.Hpk_idHotel,
                                Ddescription  = p.Ddescription,
                                DendDate    = p.DendDate,  
                                Dpercentage= p.Dpercentage,
                                Dprice= p.Dprice,
                                DstartDate=p.DstartDate,
                                Hname=p.Hname,
                                Tname=p.Tname
                            };
                var products = query.ToList().Select(r => new vDiscount
                {
                    Hpk_idHotel = r.Hpk_idHotel,
                    Ddescription = r.Ddescription,
                    DendDate = r.DendDate,
                    Dpercentage = r.Dpercentage,
                    Dprice = r.Dprice,
                    DstartDate = r.DstartDate,
                    Hname = r.Hname,
                    Tname = r.Tname

                }).ToList();
                ret = products;
                #endregion
            }
            return ret;
        }

        
    }   
        
    
}