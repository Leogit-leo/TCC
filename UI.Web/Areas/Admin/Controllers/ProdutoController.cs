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
    public class ProdutoController : Controller
    {
        BancoContexto contexto;

        public ProdutoController()
        {
            contexto = new BancoContexto();
            ViewBag.Controller = "Produto";
            ViewBag.TitulosForm = "Produto";
            ViewBag.tblDinamico = "tblProdutos";
            ViewBag.Status = contexto.Status.ToList();
           
        }

        public ActionResult Index(int? page, string s)
        {
            s = (s != null) ? s.ToLower() : null;

            var Produtos = contexto.Produto.Where(x => x.Nome.ToLower().Contains(s) || s == null).OrderByDescending(x => x.ProdutoId).ToPagedList(page ?? 1, 10);
            return View(Produtos);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Create(Produto Produto)
        {
            var Retorno = new RetornoJson();

            if (Produto.Nome == null)
                Retorno.Mensagem += "<span> Inserir o Produto</span>";
            if (Produto.Preco <= 0)
                Retorno.Mensagem += "<span> Inserir um Preço valido</span>";
            if (Produto.Quantidade <= 0)
                Retorno.Mensagem += "<span> Inserir Quantidade</span>";

            if(Retorno.Mensagem != "")
                return Json(Retorno, JsonRequestBehavior.AllowGet);

            try
            {
                var bdProduto = new ProdutoRepositorioEF(contexto);
                bdProduto.Adicionar(Produto);
                bdProduto.SalvarTodos();

                Retorno.Mensagem += "<span> Cadastrado com sucesso</span>";
                Retorno.Sucesso = true;
                Retorno.Redirecionar = true;
                Retorno.Link = "/Admin/Produto/Index";
            }
            catch (Exception e)
            {
                Retorno.Mensagem += "<span> Cadastrado nao foi completado</span>";
            }
            return Json(Retorno, JsonRequestBehavior.AllowGet);

        }


        public ActionResult Edit(int? id)
        {
            var Produto = contexto.Produto.Where(x => x.ProdutoId == id).FirstOrDefault();
            if (Produto == null)
                return RedirectToAction("Index");
            return View(Produto);
        }

        [HttpPost]
        public JsonResult Edit(Produto Produto)
        {
            var Retorno = new RetornoJson();
            if (Produto.Nome == null)
                Retorno.Mensagem += "<span> Inserir o Produto</span>";
            if (Produto.Preco <= 0)
                Retorno.Mensagem += "<span> Inserir um Preço valido</span>";
            if (Produto.Quantidade <= 0)
                Retorno.Mensagem += "<span> Inserir Quantidade</span>";

            if (Retorno.Mensagem != "")
                return Json(Retorno, JsonRequestBehavior.AllowGet);

            var bdProduto = new ProdutoRepositorioEF(contexto);
            bdProduto.Atualizar(Produto);
            bdProduto.SalvarTodos();

            Retorno.Mensagem += "<span> Editado com sucesso</span>";

            Retorno.Sucesso = true;
            Retorno.Redirecionar = true;
            Retorno.Link = "/Admin/Produto/Index";

            return Json(Retorno, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult EditarEstoque(int id)
        {
            var Produto = contexto.Produto.Where(x => x.ProdutoId == id).FirstOrDefault();
            return PartialView(Produto);
        }

        [HttpPost]
        public JsonResult EditarEstoque(Produto produto)
        {
            var Retorno = new RetornoJson();
            if (produto.Quantidade <= 0)
                Retorno.Mensagem += "<span>Inserir uma Quantidade Maior que ZERO</span>";
          
            if(produto.StatusId.Equals(2))
                Retorno.Mensagem += "<span>O Produto deve está ativo para continuar</span>";

            if (Retorno.Mensagem != "")
                return Json(Retorno, JsonRequestBehavior.AllowGet);

            var bdProduto = new ProdutoRepositorioEF(contexto);
            var ProdutoAtualizar = contexto.Produto.Where(x => x.ProdutoId == produto.ProdutoId).FirstOrDefault();
            ProdutoAtualizar.Quantidade = ProdutoAtualizar.Quantidade + produto.Quantidade;
            bdProduto.Atualizar(ProdutoAtualizar);
            bdProduto.SalvarTodos();

            Retorno.Sucesso = true;
            Retorno.Redirecionar = true;
            Retorno.Link = "/Admin/Produto/Index";
            return Json(Retorno, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MudarStatus(int id)
        {
            using (var bdProduto = new ProdutoRepositorioEF(contexto))
            {
                var produto = contexto.Produto.FirstOrDefault(x => x.ProdutoId == id);

                produto.StatusId = produto.StatusId == 1 ? 2 : 1;
                bdProduto.Atualizar(produto);
                bdProduto.SalvarTodos();
            }

            return RedirectToAction("Index");
        }

    }
}