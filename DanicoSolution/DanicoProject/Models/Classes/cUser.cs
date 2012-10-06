using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DanicoProject.Models.Classes
{
    public class cUser
    {
        public string name {get; set;}
        public string email { get; set; }
        public cUser() { 
        }

        public long insertUser() {

            long id = -1;
            UserT user = new UserT() { name = this.name ,
                                       email = this.email
            };
            using (DanicoProject.Models.AllConection tmp = new Models.AllConection())
            {
                tmp.UserTs.AddObject(user);
                tmp.SaveChanges();
                id = user.pk_idUser;
            }

            return id;
        }

    }
}