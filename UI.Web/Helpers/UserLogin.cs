using Aplicacao.Core.Dominio;
using Aplicacao.Core.RepositorioEF.Contexto;
using System;
using System.Linq;
using System.Web;
using System.Web.Security;


namespace UI.Web.Helpers
{
    public static class UserLogin
    {

        internal static void SetUsuarioAdmin(Funcionario usuario)
        {
            System.Web.HttpContext.Current.Session["Usuario"] = usuario.FuncionarioId;
            System.Web.HttpContext.Current.Session["AreaLogin"] = "Admin";

            var userCookie = new HttpCookie("FuncionarioId", usuario.FuncionarioId.ToString());
            userCookie.Expires.AddDays(365);
            HttpContext.Current.Response.SetCookie(userCookie);
        }

        public static Funcionario GetUsuarioAdmin()
        {
            BancoContexto ctx = new BancoContexto();
            Funcionario Usuario = null;

            if (HttpContext.Current.Session["FuncionarioId"] != null)
            {
                int id = int.Parse(HttpContext.Current.Session["FuncionarioId"].ToString());
                Usuario = ctx.Funcionario.Where(x => x.FuncionarioId == id).FirstOrDefault();
            }

            if (Usuario == null)
            {
                HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies["FuncionarioId"];
                if (cookie != null)
                {
                    Usuario = ctx.Funcionario.Where(x => x.FuncionarioId.ToString() == cookie.Value.ToString()).FirstOrDefault();
                    if (Usuario != null)
                        SetUsuarioAdmin(Usuario);
                }
            }

            if (Usuario == null)
                LogoutUser();

            return Usuario;
        }

        internal static void LogoutUser()
        {
            if (System.Web.HttpContext.Current.Request.Cookies["FuncionarioId"] != null)
            {
                var user = new HttpCookie("user")
                {
                    Expires = DateTime.Now.AddDays(-1),
                    Value = null
                };
                var userCookie = new HttpCookie("FuncionarioId", null);
                userCookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.SetCookie(userCookie);
            }

            FormsAuthentication.SignOut();
            HttpContext.Current.Session.Clear();
        }

        internal static int GetUserID()
        {
            return int.Parse(System.Web.HttpContext.Current.Session["FuncionarioId"].ToString());
        }
    }
}