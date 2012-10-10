using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DanicoProject.Models.Classes
{
    public class cUser
    {
      
        UserT user = new UserT();
        public cUser(UserT user)
        {
            this.user = user;
        }

        public cUser()
        {
        }

        public long insertUser() {

            long id = -1;
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