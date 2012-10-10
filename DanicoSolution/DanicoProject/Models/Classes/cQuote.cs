using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DanicoProject.Models.Classes
{
    public class cQuote
    {

        Quote quote = new Quote();

        public  cQuote(Quote quote)
        {
            this.quote = quote;
        }

        public  cQuote()
        {
        }

        internal long insertQuote()
        {
            long id = -1;
            using (DanicoProject.Models.AllConection tmp = new Models.AllConection())
            {

                tmp.Quotes.AddObject(quote);
                tmp.SaveChanges();
                id = quote.pk_idQuote;
            }
            return id;
        }
    }
}