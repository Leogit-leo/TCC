using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using UI.Web.Helpers;

namespace UI.Web.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class OnlyAdminAttibute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //var isAuthorized = base.AuthorizeCore(httpContext);
            //if (!isAuthorized)
            //    return false;

            var UsuarioLogad = UserLogin.GetUsuarioAdmin();
            if (UsuarioLogad != null && UsuarioLogad.Tipo =="Gerente")
                return true;
            else
                return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
            new RouteValueDictionary(
                new
                {
                    controller = "Home",
                    action = "AcessoNegado",
                })
            );
        }

    }
}