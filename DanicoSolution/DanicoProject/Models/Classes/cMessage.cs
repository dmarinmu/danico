using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DanicoProject.Models.Classes
{
    public class cMessage
    {
        public string From{ get; set; }
        public string To{ get; set; }
        public string Subject{ get; set; }
        public string Body{ get; set; }

       
        public cMessage(string From, string To, string Subject, string Body) { 
            this.From = From;
            this.To = To;
            this.Subject = Subject;
            this.Body = Body;
        }

        public bool send()
        {
            bool result = false;
            try
            {
                result = QiHe.CodeLib.Net.EmailSender.Send(
                  From,
                  To,
                  Subject,
                  Body);
            }
            catch (Exception)
            {

                result = false;
            }
           
            return result;
        
        }
    }
}