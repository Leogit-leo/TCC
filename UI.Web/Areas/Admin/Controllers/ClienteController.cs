using Aplicacao.Core.Dominio;
using Aplicacao.Core.RepositorioEF;
using Aplicacao.Core.RepositorioEF.Contexto;
using PagedList;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Web.Attributes;
using UI.Web.Helpers;

namespace UI.Web.Areas.Admin.Controllers
{
    [OnlyAdminAttibute]
    [ValidateInput(false)]
    [CustomAuthorize]
    public class ClienteController : Controller
    {
        BancoContexto contexto;

        public ClienteController()
        {
            contexto = new BancoContexto();
            ViewBag.Controller = "Cliente";
            ViewBag.TitulosForm = "Cliente";
            ViewBag.tblDinamico = "tblClientes";
            ViewBag.Status = contexto.Status.ToList();

        }

        public ActionResult Index(int? page, string s)
        {
            s = (s != null) ? s.ToLower() : null;

            var Clientes = contexto.Cliente.Where(x => x.Nome.ToLower().Contains(s) || s == null).OrderByDescending(x => x.ClienteId).ToPagedList(page ?? 1, 10);
            return View(Clientes);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Create(Cliente Cliente)
        {
            var Retorno = new RetornoJson();

            if (Cliente.Nome == null)
                Retorno.Mensagem += "<span> Digite o Nome</span>";
            if (Cliente.CPF == null)
                Retorno.Mensagem += "<span> Digite o CPF</span>";
            if (Cliente.Endereco == null)
                Retorno.Mensagem += "<span> Digite o Endereço</span>";
            if (Cliente.Telefone == null)
                Retorno.Mensagem += "<span> Digite o Telefone</span>";
            if (Cliente.Email == null)
                Retorno.Mensagem += "<span> Digite o Email</span>";
            if (Retorno.Mensagem != "")
                return Json(Retorno, JsonRequestBehavior.AllowGet);

            try
            {
                var bdCliente = new ClienteRepositorioEF(contexto);
                bdCliente.Adicionar(Cliente);
                bdCliente.SalvarTodos();

                Retorno.Mensagem += "<span> Cadastrado com sucesso</span>";
                Retorno.Sucesso = true;
                Retorno.Redirecionar = true;
                Retorno.Link = "/Admin/Cliente/Index";
            }
            catch (Exception e)
            {
                Retorno.Mensagem += "<span> Cliente não cadastrado.</span>";
            }
            return Json(Retorno, JsonRequestBehavior.AllowGet);

        }


        public ActionResult Edit(int? id)
        {
            var Cliente = contexto.Cliente.Where(x => x.ClienteId == id).FirstOrDefault();
            if (Cliente == null)
                return RedirectToAction("Index");
            return View(Cliente);
        }

        [HttpPost]
        public JsonResult Edit(Cliente Cliente , string HiddenSenha)
        {
            var Retorno = new RetornoJson();

            if (Cliente.Nome == null)
                Retorno.Mensagem += "<span> Digite o Nome</span>";
            if (Cliente.CPF == null)
                Retorno.Mensagem += "<span> Digite o CPF</span>";
            if (Cliente.Endereco == null)
                Retorno.Mensagem += "<span> Digite o Endereço</span>";
            if (Cliente.Telefone == null)
                Retorno.Mensagem += "<span> Digite o Telefone</span>";
            if (Cliente.Email == null)
                Retorno.Mensagem += "<span> Digite o Email</span>";
            if (Retorno.Mensagem != "")
                return Json(Retorno, JsonRequestBehavior.AllowGet);

            var bdCliente = new ClienteRepositorioEF(contexto);

            if (Cliente.Login != null && HiddenSenha != "")
            {
                Cliente.Senha = HiddenSenha;
            }
          
            bdCliente.Atualizar(Cliente);
            bdCliente.SalvarTodos();

            Retorno.Mensagem += "<span> Editado com sucesso</span>";

            Retorno.Sucesso = true;
            Retorno.Redirecionar = true;
            Retorno.Link = "/Admin/Cliente/Index";

            return Json(Retorno, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MudarStatus(int id)
        {
            var cliente = contexto.Cliente.FirstOrDefault(x => x.ClienteId == id);
            cliente.StatusId = cliente.StatusId == 1 ? 2 : 1;

            using(var bdCliente = new ClienteRepositorioEF(contexto))
            {
                bdCliente.Atualizar(cliente);
                bdCliente.SalvarTodos();
            }

            return RedirectToAction("Index");
        }

    }
}