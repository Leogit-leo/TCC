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
    public class ServicoController : Controller
    {
        BancoContexto contexto;

        public ServicoController()
        {
            contexto = new BancoContexto();
            ViewBag.Controller = "Servico";
            ViewBag.TitulosForm = "Servico";
            ViewBag.tblDinamico = "tblServicos";
            ViewBag.Status = contexto.Status.ToList();

        }
      
        public ActionResult Index(int? page, string s)
        {
            s = (s != null) ? s.ToLower() : null;

            var Servicos = contexto.Servico.Where(x => x.Nomeservico.ToLower().Contains(s) || s == null).OrderByDescending(x => x.ServicoId).ToPagedList(page ?? 1, 10);
            return View(Servicos);
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Create(Servico Servico)
        {
            var Retorno = new RetornoJson();

            if (Servico.Nomeservico == null)
                Retorno.Mensagem += "<span> Inserir o Nome do Servico</span>";
            if (Servico.Preco == 0 || Servico.Preco < 0)
                Retorno.Mensagem += "<span> Digite um Preço valido </span>";
            if (Servico.TempoGasto == 0 || Servico.TempoGasto < 0)
                Retorno.Mensagem += "<span> Digite um Tempo </span>";
            if (Retorno.Mensagem != "")
                return Json(Retorno, JsonRequestBehavior.AllowGet);

            try
            {
                var bdServico = new ServicoRepositorioEF(contexto);
                bdServico.Adicionar(Servico);
                bdServico.SalvarTodos();

                Retorno.Mensagem += "<span> Cadastrado com sucesso</span>";
                Retorno.Sucesso = true;
                Retorno.Redirecionar = true;
                Retorno.Link = "/Admin/Servico/Index";
            }
            catch (Exception e)
            {
                Retorno.Mensagem += "<span> Não foi possivel persistir seu cadastro</span>";
            }
            return Json(Retorno, JsonRequestBehavior.AllowGet);

        }


        public ActionResult Edit(int? id)
        {
            var Servico = contexto.Servico.Where(x => x.ServicoId == id).FirstOrDefault();
            if (Servico == null)
                return RedirectToAction("Index");
            return View(Servico);
        }

        [HttpPost]
        public JsonResult Edit(Servico Servico)
        {
            var Retorno = new RetornoJson();
            if (Servico.Nomeservico == null)
                Retorno.Mensagem += "<span> Inserir o Nome do Servico</span>";
            if (Servico.Preco == 0 || Servico.Preco < 0)
                Retorno.Mensagem += "<span> Digite um Preço valido </span>";
            if (Servico.TempoGasto == 0 || Servico.TempoGasto < 0)
                Retorno.Mensagem += "<span> Digite um Tempo </span>";
            if (Retorno.Mensagem != "")
                return Json(Retorno, JsonRequestBehavior.AllowGet);

            var bdServico = new ServicoRepositorioEF(contexto);
            bdServico.Atualizar(Servico);
            bdServico.SalvarTodos();

            Retorno.Mensagem += "<span> Editado com sucesso</span>";

            Retorno.Sucesso = true;
            Retorno.Redirecionar = true;
            Retorno.Link = "/Admin/Servico/Index";

            return Json(Retorno, JsonRequestBehavior.AllowGet);
        }

        public ActionResult  MudarStatus(int id)
        {
           
            using (var bdServico = new ServicoRepositorioEF(contexto))
            {
                var servico = contexto.Servico.FirstOrDefault(x => x.ServicoId == id);

                servico.StatusId = servico.StatusId == 1 ? 2 : 1;
                bdServico.Atualizar(servico);
                bdServico.SalvarTodos();
            }

            return RedirectToAction("Index");
        }

    }
}