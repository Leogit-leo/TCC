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
    public class CategoriaFotoController : Controller
    {
        BancoContexto contexto;

        public CategoriaFotoController()
        {
            contexto = new BancoContexto();
            ViewBag.Controller = "CategoriaFoto";
            ViewBag.TitulosForm = "CategoriaFoto";
            ViewBag.tblDinamico = "tblCategoriaFotos";
            ViewBag.Status = contexto.Status.ToList();
        }

        public ActionResult Index()
        {
            var CategoriaFoto = contexto.CategoriaFoto.ToList();
            return View(CategoriaFoto);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Create(CategoriaFoto CategoriaFoto)
        {
            var Retorno = new RetornoJson();

            if (CategoriaFoto.Nome == null)
                Retorno.Mensagem += "<span> Digite o Nome</span>";
          
            if(Retorno.Mensagem != "")
                return Json(Retorno, JsonRequestBehavior.AllowGet);

            try
            {
                var bdCategoriaFoto = new CategoriaFotoRepositorioEF(contexto);
                bdCategoriaFoto.Adicionar(CategoriaFoto);
                bdCategoriaFoto.SalvarTodos();

                Retorno.Mensagem += "<span> Cadastrado com sucesso</span>";
                Retorno.Sucesso = true;
                Retorno.Redirecionar = true;
                Retorno.Link = "/Admin/CategoriaFoto/Index";
            }catch(Exception e)
            {
                Retorno.Mensagem += "<span> CategoriaFoto não cadastrado.</span>";
            }
            return Json(Retorno, JsonRequestBehavior.AllowGet);

        }


        public ActionResult Edit(int? id)
        {
            var CategoriaFoto = contexto.CategoriaFoto.Where(x => x.CategoriaFotoId == id).FirstOrDefault();
            if (CategoriaFoto == null)
                return RedirectToAction("Index");
            return View(CategoriaFoto);
        }

        [HttpPost]
        public JsonResult Edit(CategoriaFoto CategoriaFoto)
        {
            var Retorno = new RetornoJson();

            if (CategoriaFoto.Nome == null)
                Retorno.Mensagem += "<span> Digite o Nome</span>";
           
            if (Retorno.Mensagem != "")
                return Json(Retorno, JsonRequestBehavior.AllowGet);

            var bdCategoriaFoto = new CategoriaFotoRepositorioEF(contexto);
            bdCategoriaFoto.Atualizar(CategoriaFoto);
            bdCategoriaFoto.SalvarTodos();

            Retorno.Mensagem += "<span> Editado com sucesso</span>";

            Retorno.Sucesso = true;
            Retorno.Redirecionar = true;
            Retorno.Link = "/Admin/CategoriaFoto/Index";

            return Json(Retorno, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int id)
        {
            var bdCategoriaFoto = new CategoriaFotoRepositorioEF(contexto);
            bdCategoriaFoto.Excluir(x => x.CategoriaFotoId == id);
            bdCategoriaFoto.SalvarTodos();
            return RedirectToAction("Index");
        }

    }
}