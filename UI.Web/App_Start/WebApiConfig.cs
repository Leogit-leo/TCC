using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace UI.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}", /*LBA 27/09 adicionei action na rotas da API*/
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
