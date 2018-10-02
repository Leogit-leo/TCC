using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using UI.Web.Helpers;

namespace UI.Web.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //var isAuthorized = base.AuthorizeCore(httpContext);
            //if (!isAuthorized)
            //    return false; 

            if (httpContext.Session["Usuario"] != null)
                return true;
            else
                return false;
            //if (UserLogin.GetUsuarioAdmin() != null)
            //    return true;
            //else
            //    return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
            new RouteValueDictionary(
                new
                {
                    controller = "Login",
                    action = "Index",
                })
            );
        }

    }
}