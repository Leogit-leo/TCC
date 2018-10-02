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
    public class StatusController : Controller
    {
        BancoContexto contexto;

        public StatusController()
        {
            contexto = new BancoContexto();
            ViewBag.Controller = "Status";
            ViewBag.TitulosForm = "Status";
            ViewBag.tblDinamico = "tblStatuss";
           
        }

        public ActionResult Index()
        {
            var Status = contexto.Status.ToList();
            return View(Status);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Create(Status Status)
        {
            var Retorno = new RetornoJson();

            if (Status.Nome == null)
                Retorno.Mensagem += "<span> Digite o Nome</span>";
          
            if(Retorno.Mensagem != "")
                return Json(Retorno, JsonRequestBehavior.AllowGet);

            try
            {
                var bdStatus = new StatusRepositorioEF(contexto);
                bdStatus.Adicionar(Status);
                bdStatus.SalvarTodos();

                Retorno.Mensagem += "<span> Cadastrado com sucesso</span>";
                Retorno.Sucesso = true;
                Retorno.Redirecionar = true;
                Retorno.Link = "/Admin/Status/Index";
            }catch(Exception e)
            {
                Retorno.Mensagem += "<span> Status não cadastrado.</span>";
            }
            return Json(Retorno, JsonRequestBehavior.AllowGet);

        }


        public ActionResult Edit(int? id)
        {
            var Status = contexto.Status.Where(x => x.StatusId == id).FirstOrDefault();
            if (Status == null)
                return RedirectToAction("Index");
            return View(Status);
        }

        [HttpPost]
        public JsonResult Edit(Status Status)
        {
            var Retorno = new RetornoJson();

            if (Status.Nome == null)
                Retorno.Mensagem += "<span> Digite o Nome</span>";
           
            if (Retorno.Mensagem != "")
                return Json(Retorno, JsonRequestBehavior.AllowGet);

            var bdStatus = new StatusRepositorioEF(contexto);
            bdStatus.Atualizar(Status);
            bdStatus.SalvarTodos();

            Retorno.Mensagem += "<span> Editado com sucesso</span>";

            Retorno.Sucesso = true;
            Retorno.Redirecionar = true;
            Retorno.Link = "/Admin/Status/Index";

            return Json(Retorno, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int id)
        {
            var bdStatus = new StatusRepositorioEF(contexto);
            bdStatus.Excluir(x => x.StatusId == id);
            bdStatus.SalvarTodos();
            return RedirectToAction("Index");
        }

    }
}