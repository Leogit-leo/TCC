using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Linq;
using System.Web;


namespace UI.Web.Areas.Admin.API
{
    public class ConectarBD: DataContext
    {
        public ConectarBD(string cnx):base(cnx)
        {

        }
       
    }
}