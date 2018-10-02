using Aplicacao.Core.Dominio;
using Aplicacao.Core.RepositorioEF.Contexto;
using System.Linq;
using System.Web.Mvc;
using UI.Web.Helpers;

namespace UI.Web.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        BancoContexto contexto;

        public LoginController()
        {
            contexto = new BancoContexto();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logout()
        {
            UI.Web.Helpers.UserLogin.LogoutUser();

            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public JsonResult LogarNoSistema(Funcionario funcionario)
        {
            var retorno = new RetornoJson();


            retorno.Sucesso = false;

            if (funcionario.Login == null)
                retorno.Mensagem += "<span> Digite seu Login </span>";
            if (funcionario.Senha == null)
                retorno.Mensagem += "<span> Digite sua Senha </span>";

            var Usuario = contexto.Funcionario.Where(x => x.Login == funcionario.Login && x.Senha == funcionario.Senha && x.Tipo.Equals("Gerente")).FirstOrDefault();
         
            if (Usuario == null)
            {
                retorno.Mensagem += "<span> Usuário ou senha incorreto</span>";
            }
            else
            {
                UserLogin.SetUsuarioAdmin(Usuario);
                //Session["Usuario"] = Usuario;
                retorno.Sucesso = true;
                retorno.Redirecionar = true;
                retorno.Link = Url.Action("Index", "Home");
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
     
    }
}
