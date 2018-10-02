using Aplicacao.Core.Dominio;
using Aplicacao.Core.RepositorioEF;
using Aplicacao.Core.RepositorioEF.Contexto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using UI.Web.Attributes;

namespace UI.Web.Areas.Admin.Controllers
{
    [OnlyAdminAttibute]
    [ValidateInput(false)]
    [CustomAuthorize]
    public class CompraController : Controller
    {
        BancoContexto contexto;

        public CompraController()
        {
            contexto = new BancoContexto();

        }

        public ActionResult Index()
        {
            ViewBag.Cliente = contexto.Cliente.Where(x=> x.Status.Nome.Equals("Ativo")).OrderBy(x => x.Nome).ToList();
            ViewBag.Produto = contexto.Produto.Where(x => x.Quantidade > 0).OrderBy(x => x.Nome).ToList();
            var CompraProduto = contexto.RegistroCompra.OrderBy(x => x.DataCompra).ToList();
            return View(CompraProduto);
        }


        public ActionResult Create(string s)
        {
            ViewBag.ItensCompra = contexto.ItemCompra.Where(x => x.RegistroCompra.Cliente.Nome.Contains(s) ||
            x.RegistroCompra.Cliente.CPF.Contains(s)).OrderByDescending(x => x.RegistroCompra.DataCompra).ToList();

            ViewBag.Cliente = contexto.Cliente.Where(x => x.Status.Nome.Equals("Ativo")).OrderBy(x => x.Nome).ToList();
            ViewBag.Produto = contexto.Produto.Where(x => x.Quantidade > 0 && x.Status.Nome.Equals("Ativo")).OrderBy(x => x.Nome).ToList();
            return View();
        }

        public class ProdValido
        {
            public int idProd { get; set; }
            public int quant { get; set; }
        }

        public JsonResult RegistrarCompra(RegistroCompra compra, FormCollection collection)
        {
            var retorno = new RetornoJson();
            if (compra.ClienteId == 0)
                retorno.Mensagem += "<span>Selecione um Cliente</span>";
            if (retorno.Mensagem != "")
                return Json(retorno, JsonRequestBehavior.AllowGet);

            if (collection["ProdutosSelecionados"] != null)
            {
                var Produtos = collection["ProdutosSelecionados"].Split(',');
                var Estoque = contexto.Produto.ToList();
                var ProdutosValido = new List<ProdValido>();

                foreach (var p in Produtos)
                {
                    var idProduto = int.Parse(p);
                    var qtde = int.Parse(collection["Quantidade_Produto_" + p]);
                    //validação importante que não permite valor negativo
                    if (qtde < 0)
                    {
                        retorno.Mensagem = "<span>Valor não pode ser Negativo</span>";
                        if (retorno.Mensagem != "")
                            return Json(retorno, JsonRequestBehavior.AllowGet);
                    }

                    if (Estoque.Any(x => x.ProdutoId == idProduto && x.Quantidade >= qtde))
                    {
                        ProdutosValido.Add(new ProdValido()
                        {
                            idProd = idProduto,
                            quant = qtde
                        });

                        //subtrai a quantidade que foi comprada
                        var bdProduto = new ProdutoRepositorioEF(contexto);
                        var ProdutoAtualizar = contexto.Produto.Where(x => x.ProdutoId == idProduto).FirstOrDefault();
                        ProdutoAtualizar.Quantidade = ProdutoAtualizar.Quantidade - qtde;
                        bdProduto.Atualizar(ProdutoAtualizar);
                        bdProduto.SalvarTodos();

                    }
                    else
                    {
                        retorno.Mensagem += "<span>Sua compra ultrapassa a quantidade em estoque</span>";
                    }

                }// valida o estoque e cria lista de produtos para compra

                if (retorno.Mensagem != "")
                    return Json(retorno, JsonRequestBehavior.AllowGet);



                if (retorno.Mensagem == "")
                {
                    compra.DataCompra = DateTime.Now;
                    var bdRegistroCompra = new RegistroCompraRepositorioEF(contexto);
                    bdRegistroCompra.Adicionar(compra);
                    bdRegistroCompra.SalvarTodos();

                    // adiciona os itens da compra
                    var bdItemCompra = new ItemCompraRepositorioEF(contexto);
                    foreach (var produto in ProdutosValido)
                    {
                        bdItemCompra.Adicionar(new ItemCompra()
                        {
                            RegistroCompraId = compra.RegistroCompraId,
                            ProdutoId = produto.idProd,
                            Quantidade = produto.quant
                        });

                    }
                    bdItemCompra.SalvarTodos();

                    retorno.Sucesso = true;
                    retorno.LimparForm = true;
                    retorno.Redirecionar = true;
                    retorno.Link = "/Admin/Compra/Index";
                }
            }
            else
            {
                retorno.Mensagem += "<span> Não foi selecionado nenhuma Compra</span>";
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult LoadFormEditCompra(int id)
        {
            ViewBag.Clientes = contexto.Cliente.Where(x => x.Status.Nome.Equals("Ativo")).OrderBy(x => x.Nome).ToList();
            ViewBag.Produtos = contexto.Produto.OrderBy(x => x.Nome).ToList();

            var Compra = contexto.RegistroCompra.Find(id);
            return PartialView(Compra);
        }

        [HttpPost]
        public JsonResult EditarCompra(RegistroCompra compra, FormCollection collection)
        {
            var Retorno = new RetornoJson();
            var ProdutosSalvoBancoDados = contexto.ItemCompra.Where(x => x.RegistroCompraId == compra.RegistroCompraId).ToList();
            var bdItemCompra = new ItemCompraRepositorioEF(contexto);
            var bdProduto = new ProdutoRepositorioEF(contexto);

            var Compra = contexto.RegistroCompra.Where(x => x.RegistroCompraId == compra.RegistroCompraId).FirstOrDefault();
            Compra.ClienteId = compra.ClienteId;
            var bdRegistroCompra = new RegistroCompraRepositorioEF(contexto);
            bdRegistroCompra.Atualizar(Compra);
            bdRegistroCompra.SalvarTodos();

            //se nao for selecionado nenhum produto exclui todos os itens de compra
            if (collection["ProdutosSelecionados"] == null)
            {
                Retorno.Mensagem = "<span> Selecione a Compra</span>";
                if (Retorno.Mensagem != "")
                    return Json(Retorno, JsonRequestBehavior.AllowGet);

                //var ItensDevolver = contexto.ItemCompra.Where(x => x.RegistroCompraId == compra.RegistroCompraId).ToList();
                //var Produtos = contexto.Produto.ToList();

                //foreach (var produto in Produtos)
                //{
                //    var Produto = contexto.Produto.Where(x => x.ProdutoId == produto.ProdutoId).FirstOrDefault();
                //    if (ItensDevolver.Any(x => x.ProdutoId == produto.ProdutoId))
                //    {
                //        int QtdeDevolver = ItensDevolver.Where(x => x.ProdutoId == produto.ProdutoId).FirstOrDefault().Quantidade;
                //        Produto.Quantidade = Produto.Quantidade + QtdeDevolver;
                //        bdProduto.Atualizar(Produto);
                //    }
                //}
                //bdProduto.SalvarTodos();

                //bdItemCompra.Excluir(x => x.RegistroCompraId == compra.RegistroCompraId);
                //bdItemCompra.SalvarTodos();
            }
            else
            {


                if (collection["ProdutosSelecionados"] != null)
                {
                    var ProdSelecionado = collection["ProdutosSelecionados"].Split(',');
                    var Estoque = contexto.Produto.ToList();

                    // se tem no banco de dados mas não tem na lista
                    foreach (var produtoBanco in ProdutosSalvoBancoDados)
                    {
                        if (!ProdSelecionado.Any(x => x == produtoBanco.ProdutoId.ToString()))
                        {
                            //voltar a quantidade para o estoque de produtos
                            int QtdeProdutoDevolver = produtoBanco.Quantidade;
                            var Produto = contexto.Produto.Where(x => x.ProdutoId == produtoBanco.ProdutoId).FirstOrDefault();
                            Produto.Quantidade = Produto.Quantidade + QtdeProdutoDevolver;
                            bdProduto.Atualizar(Produto);
                            bdProduto.SalvarTodos();
                            //exclui os produtos que não estao selecionados na edição
                            bdItemCompra.Excluir(x => x.ProdutoId == produtoBanco.ProdutoId && x.RegistroCompraId == produtoBanco.RegistroCompraId);
                            bdItemCompra.SalvarTodos();
                        }
                    }

                    //se tiver na lista e nao tiver no banco salva
                    foreach (var prodSelecionado in ProdSelecionado)
                    {
                        var idProdSelecionado = int.Parse(prodSelecionado);
                        var qtde = int.Parse(collection["Quantidade_Produto_" + idProdSelecionado]);
                        //validação importante que não permite valor negativo
                        if (qtde < 0)
                        {
                            Retorno.Mensagem = "<span>Valor não pode ser Negativo</span>";
                            if (Retorno.Mensagem != "")
                                return Json(Retorno, JsonRequestBehavior.AllowGet);
                        }

                        if (!ProdutosSalvoBancoDados.Any(x => x.RegistroCompraId == compra.RegistroCompraId && x.ProdutoId == idProdSelecionado))
                        {
                            //validacao se tem no estoque
                            if (Estoque.Any(x => x.ProdutoId == idProdSelecionado && x.Quantidade >= qtde))
                            {
                                //subtrair produtos que foram comprados ao editar a compra
                                var Produto = contexto.Produto.Where(x => x.ProdutoId == idProdSelecionado).FirstOrDefault();
                                Produto.Quantidade = Produto.Quantidade - qtde;
                                bdProduto.Atualizar(Produto);
                                bdProduto.SalvarTodos();
                                // adicionar a compra do produto 
                                bdItemCompra.Adicionar(new ItemCompra()
                                {
                                    ProdutoId = idProdSelecionado,
                                    Quantidade = qtde,
                                    RegistroCompraId = compra.RegistroCompraId
                                });
                                bdItemCompra.SalvarTodos();
                            }
                            else
                            {
                                Retorno.Mensagem += "<span>Produto excede a quantidade ou não tem no estoque</span>";
                            }

                        }

                        // se na lista tem e  no banco tem, mas quer trocar a quantidade
                        if (ProdutosSalvoBancoDados.Any(x => x.RegistroCompraId == compra.RegistroCompraId && x.ProdutoId == idProdSelecionado && x.Quantidade != qtde))
                        {

                            //subtrair ou adicionar a nova quantidade no estoque de produto
                            var ProdutoAtual = ProdutosSalvoBancoDados.Where(x => x.RegistroCompraId == compra.RegistroCompraId &&
                            x.ProdutoId == idProdSelecionado).FirstOrDefault();
                            int qtdeAntiga = ProdutoAtual.Quantidade;
                            var Produto = contexto.Produto.Where(x => x.ProdutoId == idProdSelecionado).FirstOrDefault();
                            int nova;

                            if (qtde > qtdeAntiga)
                            {
                                nova = qtde - qtdeAntiga;
                                if (Estoque.Any(x => x.ProdutoId == idProdSelecionado && x.Quantidade >= nova))
                                {
                                    //subtrai do estoque de produto
                                    Produto.Quantidade = Produto.Quantidade - nova;
                                    bdProduto.Atualizar(Produto);
                                    bdProduto.SalvarTodos();

                                    //atualiza a nova quantidade setada na compra do produto
                                    var ProdNovaQtde = contexto.ItemCompra.Where(x => x.RegistroCompraId == compra.RegistroCompraId &&
                                    x.ProdutoId == idProdSelecionado).FirstOrDefault();

                                    ProdNovaQtde.Quantidade = qtde;
                                    bdItemCompra.Atualizar(ProdNovaQtde);
                                    bdItemCompra.SalvarTodos();
                                }
                                else
                                {
                                    Retorno.Mensagem += "<span>Produto excede a quantidade ou não tem no estoque</span>";
                                }
                            }

                            else
                            {
                                nova = qtdeAntiga - qtde;
                                //devolve a quantidade para o estoque de produto
                                Produto.Quantidade = Produto.Quantidade + nova;
                                bdProduto.Atualizar(Produto);
                                bdProduto.SalvarTodos();

                                //atualiza a nova quantidade setada na compra do produto
                                var ProdNovaQtde = contexto.ItemCompra.Where(x => x.RegistroCompraId == compra.RegistroCompraId &&
                                x.ProdutoId == idProdSelecionado).FirstOrDefault();

                                ProdNovaQtde.Quantidade = qtde;
                                bdItemCompra.Atualizar(ProdNovaQtde);
                                bdItemCompra.SalvarTodos();
                            }
                           
                        }
                        
                    }

                    if (!string.IsNullOrEmpty(Retorno.Mensagem))
                        return Json(Retorno, JsonRequestBehavior.AllowGet);
                    

                    Retorno.LimparForm = true;
                    Retorno.Sucesso = true;
                    Retorno.Redirecionar = true;
                    Retorno.Link = "/Admin/Compra/Index";

                }
                else
                {
                    Retorno.Mensagem = "Não é possivel Editar uma compra sem itens";
                }

            }
            return Json(Retorno, JsonRequestBehavior.AllowGet);

        }


        //public ActionResult Delete(int id)
        //{
        //    var bdCompra = new RegistroCompraRepositorioEF(contexto);
        //    bdCompra.Excluir(x => x.RegistroCompraId == id);
        //    bdCompra.SalvarTodos();
        //    return RedirectToAction("Index");
        //}


    }
}
