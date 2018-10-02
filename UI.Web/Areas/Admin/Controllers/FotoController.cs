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
    public class FotoController : Controller
    {
        BancoContexto contexto;

        public FotoController()
        {
            contexto = new BancoContexto();
            ViewBag.Controller = "Foto";
            ViewBag.TitulosForm = "Foto";
            ViewBag.tblDinamico = "tblFotos";
            ViewBag.Status = contexto.Status.ToList();
            ViewBag.Categoria = contexto.CategoriaFoto.ToList();
            ViewBag.Funcionario = contexto.Funcionario.Where(x => x.Status.Nome == "Ativo").ToList();
        }

        public ActionResult Index()
        {
            var Foto = contexto.Foto.ToList();
            return View(Foto);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Create(Foto Foto , HttpPostedFileBase file)
        {
            var Retorno = new RetornoJson();

            if (Foto.NomeFoto == null)
                Retorno.Mensagem += "<span> Digite o Nome</span>";
           //validar status , categoria e funcionario

            if (Retorno.Mensagem != "")
                return Json(Retorno, JsonRequestBehavior.AllowGet);

            if (file != null)
            {
                string nome = "Foto-" + NomeArquivo.GerarNomeArquivo(file.FileName);
                string path = Path.Combine(Server.MapPath(Caminho.Foto()), Path.GetFileName(nome));
                file.SaveAs(path);
                Foto.Imagem = nome;
            }

            try
            {
                Foto.DataFoto = DateTime.Now;
                var bdFoto = new FotoRepositorioEF(contexto);
                bdFoto.Adicionar(Foto);
                bdFoto.SalvarTodos();

                Retorno.Mensagem += "<span> Cadastrado com sucesso</span>";
                Retorno.Sucesso = true;
                Retorno.Redirecionar = true;
                Retorno.Link = "/Admin/Foto/Index";
            }catch(Exception e)
            {
                Retorno.Mensagem += "<span> Foto não cadastrado.</span>";
            }
            return Json(Retorno, JsonRequestBehavior.AllowGet);

        }


        public ActionResult Edit(int? id)
        {
            var Foto = contexto.Foto.Where(x => x.FotoId == id).FirstOrDefault();
            if (Foto == null)
                return RedirectToAction("Index");
            return View(Foto);
        }

        [HttpPost]
        public JsonResult Edit(Foto Foto , HttpPostedFileBase file)
        {
            var Retorno = new RetornoJson();

            if (Foto.NomeFoto == null)
                Retorno.Mensagem += "<span> Digite o Nome</span>";
           //editar staus , categoria foto e funcionario
            if (Retorno.Mensagem != "")
                return Json(Retorno, JsonRequestBehavior.AllowGet);

            if (file != null)
            {
                string nome = "Foto-" + NomeArquivo.GerarNomeArquivo(file.FileName);
                string path = Path.Combine(Server.MapPath(Caminho.Foto()), Path.GetFileName(nome));
                file.SaveAs(path);
                Foto.Imagem = nome;
            }

            var bdFoto = new FotoRepositorioEF(contexto);
            bdFoto.Atualizar(Foto);
            bdFoto.SalvarTodos();

            Retorno.Mensagem += "<span> Editado com sucesso</span>";

            Retorno.Sucesso = true;
            Retorno.Redirecionar = true;
            Retorno.Link = "/Admin/Foto/Index";

            return Json(Retorno, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int id)
        {
            var bdFoto = new FotoRepositorioEF(contexto);
            bdFoto.Excluir(x => x.FotoId == id);
            bdFoto.SalvarTodos();
            return RedirectToAction("Index");
        }

    }
}