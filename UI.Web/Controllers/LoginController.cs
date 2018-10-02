using Aplicacao.Core.Dominio;
using Aplicacao.Core.RepositorioEF.Contexto;
using System.Linq;
using System.Web.Mvc;
using UI.Web.Helpers;

namespace UI.Web.Controllers
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
       
       [HttpPost]
        public JsonResult LogarFuncionario(Funcionario funcionario)
        {
            var retorno = new RetornoJson();

            retorno.Sucesso = false;

            var Usuario = contexto.Funcionario.FirstOrDefault(x => x.Login == funcionario.Login && x.Senha == funcionario.Senha && x.Tipo.Equals("Funcionario"));


            if (Usuario == null)
            {
                retorno.Mensagem += "<span> Usuário ou senha incorreto</span>";
            }
            else
            {
                UserLogin.SetUsuarioAdmin(Usuario);
               // Session["Usuario"] = Usuario;
                retorno.Sucesso = true;
                retorno.Redirecionar = true;
                retorno.Link = Url.Action("Index","Home");

            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Logout()
        {
            UserLogin.LogoutUser();
            return RedirectToAction("Index", "Login");
        }

      
    }
}
