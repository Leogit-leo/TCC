using Aplicacao.Core.Dominio;
using Aplicacao.Core.RepositorioEF;
using Aplicacao.Core.RepositorioEF.Contexto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Web.Attributes;
using UI.Web.Helpers;

namespace UI.Web.Controllers
{
    public class PrestadoresController : Controller
    {
        BancoContexto contexto;

        public PrestadoresController()
        {
            contexto = new BancoContexto();
        }

        public ActionResult Index()
        {
            ViewBag.Titulo = "Prestadores";
            ViewBag.Funcionarios = contexto.Funcionario.Where(x => x.Status.Bloquear == false).ToList();

            return View();
        }
    }
}
