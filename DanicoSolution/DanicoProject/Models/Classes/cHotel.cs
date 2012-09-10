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

    }   
        
    
}