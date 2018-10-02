using Aplicacao.Core.Dominio;
using Aplicacao.Core.RepositorioEF;
using Aplicacao.Core.RepositorioEF.Contexto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using UI.Web.Attributes;
using UI.Web.Helpers;

namespace UI.Web.Areas.Admin.Controllers
{
    [OnlyAdminAttibute]
    [ValidateInput(false)]
    [CustomAuthorize]
    public class CompraFiadoController : Controller
    {
        BancoContexto contexto;

        public CompraFiadoController()
        {
            contexto = new BancoContexto();
            ViewBag.Cliente = contexto.Cliente.Where(x => x.Status.Nome.Equals("Ativo")).OrderBy(x => x.Nome).ToList();
            ViewBag.Servicos = contexto.Servico.Where(x => x.Status.Nome.Equals("Ativo")).OrderBy(x => x.Nomeservico).ToList();
            ViewBag.Funcionarios = contexto.Funcionario.Where(x => x.Status.Nome.Equals("Ativo")).OrderBy(x => x.Nome).ToList();
            ViewBag.Produto = contexto.Produto.Where(x => x.Status.Nome.Equals("Ativo")).OrderBy(x => x.Nome).ToList();
        }

        public ActionResult Index(string s)
        {
            var CompraAPrazo = contexto.RegistroCompraFiada.Where(x => s == null || x.Cliente.Nome.Contains(s) ||
            x.Cliente.CPF.Contains(s))
                .OrderBy(x => x.DataCompra).ToList();
            return View(CompraAPrazo);
        }

        // create servicos fiado
        public ActionResult Create()
        {
            return View();
        }

        //create servicos fiado
        [HttpPost]
        public JsonResult Create(RegistroCompraFiada compraFiada, FormCollection collection)
        {
            var Retorno = new RetornoJson();
            if (compraFiada.ClienteId == 0)
                Retorno.Mensagem += "<span> O Cliente é obrigatório</span>";
            if (compraFiada.FuncionarioId == 0)
                Retorno.Mensagem += "<span> O Funcionario é obrigatório</span>";
            //validar data corretamente
            if (!Utilitarios.validarData(compraFiada.DataParaPagamento.ToString()))
                Retorno.Mensagem += "<span> A Data é obrigatório</span>";
            if (compraFiada.DataParaPagamento <= DateTime.Now || compraFiada.DataParaPagamento > DateTime.Now.AddMonths(2))
                Retorno.Mensagem += "<span> Não aceitamos data anterior ou igual a data atual ou data acima de dois meses em relação a data que o servico foi prestado</span>";

            if (Retorno.Mensagem != "")
                return Json(Retorno, JsonRequestBehavior.AllowGet);

            if (collection["Servicos"] != null)
            {
                compraFiada.DataCompra = DateTime.Now;
                var bdRegistroCompraFiada = new RegistroCompraFiadaRepositorioEF(contexto);
                bdRegistroCompraFiada.Adicionar(compraFiada);
                bdRegistroCompraFiada.SalvarTodos();

                //salvar os itens da compra de servicos fiado
                var bdServicoFiado = new ServicoFiadoRepositorioEF(contexto);

                var Servicos = collection["Servicos"].Split(',');
                foreach (var servico in Servicos)
                {
                    var servicoID = int.Parse(servico);
                    bdServicoFiado.Adicionar(new ServicoFiado()
                    {
                        RegistroCompraFiadaId = compraFiada.RegistroCompraFiadaId,
                        ServicoId = servicoID
                    });
                }
                bdServicoFiado.SalvarTodos();

                Retorno.Sucesso = true;
                Retorno.LimparForm = true;
                Retorno.Redirecionar = true;
                Retorno.Link = "/Admin/CompraFiado/Index";
            }
            else
            {
                Retorno.Mensagem += "<span> Não foi selecionado nenhum Servico</span>";
            }

            return Json(Retorno, JsonRequestBehavior.AllowGet);
        }

        // edit servicos fiado
        public PartialViewResult EditServicoFiado(int id)
        {
            ViewBag.Clientes = contexto.Cliente.Where(x => x.Status.Nome.Equals("Ativo")).OrderBy(x => x.Nome).ToList();
            ViewBag.Servicos = contexto.Servico.Where(x => x.Status.Nome.Equals("Ativo")).OrderBy(x => x.Nomeservico).ToList();
            ViewBag.Funcionarios = contexto.Funcionario.Where(x => x.Status.Nome.Equals("Ativo")).OrderBy(x => x.Nome).ToList();


            var ServicoFiado = contexto.RegistroCompraFiada.Where(x => x.RegistroCompraFiadaId == id).FirstOrDefault();
            return PartialView(ServicoFiado);
        }
        //post edit servicos fiado
        [HttpPost]
        public JsonResult EditServicoFiado(RegistroCompraFiada compraFiada, FormCollection collection)
        {
            var Retorno = new RetornoJson();
            if (!Utilitarios.validarData(compraFiada.DataParaPagamento.ToString()))
            {
                Retorno.Mensagem += "<span>Data invalida, inserir data correta</span>";
            }
            if (compraFiada.DataParaPagamento <= compraFiada.DataCompra || compraFiada.DataParaPagamento > compraFiada.DataCompra.AddMonths(2))
                Retorno.Mensagem += "<span> Não aceitamos data anterior a data da compra ou data para pagamento maior que dois meses em relação a compra</span>";

            if (Retorno.Mensagem != "")
                return Json(Retorno, JsonRequestBehavior.AllowGet);

            var bdRegistroCompraFiada = new RegistroCompraFiadaRepositorioEF(contexto);
            bdRegistroCompraFiada.Atualizar(compraFiada);
            bdRegistroCompraFiada.SalvarTodos();

            if (collection["Servicos"] == null)
            {
                Retorno.Mensagem += "<span> Não foi selecionado nenhum Servico!<span>";
                if (Retorno.Mensagem != "")
                    return Json(Retorno, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var ServicosBancoDados = contexto.ServicoFiado.Where(x => x.RegistroCompraFiadaId == compraFiada.RegistroCompraFiadaId).ToList();
                var bdServicoFiado = new ServicoFiadoRepositorioEF(contexto);
                var Servicos = collection["Servicos"].Split(',');
                //se tem na lista mas não tem no banco de dados
                foreach (var servico in Servicos)
                {
                    var idServico = int.Parse(servico);
                    if (!ServicosBancoDados.Any(x => x.ServicoId == idServico))
                    {
                        bdServicoFiado.Adicionar(new ServicoFiado()
                        {
                            RegistroCompraFiadaId = compraFiada.RegistroCompraFiadaId,
                            ServicoId = idServico
                        });
                    }
                }
                bdServicoFiado.SalvarTodos();

                // se tiver no banco de dados mas nao tiver na lista
                foreach (var banco in ServicosBancoDados)
                {
                    if (!Servicos.Any(x => x == banco.ServicoId.ToString()))
                    {
                        bdServicoFiado.Excluir(x => x.ServicoId == banco.ServicoId && x.RegistroCompraFiadaId == compraFiada.RegistroCompraFiadaId);
                    }
                }
                bdServicoFiado.SalvarTodos();

            }

            //se for pago não aparecer na lista de serviços fiado, contabilizar comissoes que o funcionario ira ganhar
            if (compraFiada.Pago == true)
            {
                var ServicosBancoDadosAposCompraPaga = contexto.ServicoFiado.Where(x => x.RegistroCompraFiadaId == compraFiada.RegistroCompraFiadaId).ToList();
                Comissao comissao = new Comissao();
                var bdComissao = new ComissaoRepositorioEF(contexto);

                foreach (var servico in ServicosBancoDadosAposCompraPaga)
                {
                    comissao.Data = DateTime.Now;
                    comissao.FuncionarioId = compraFiada.FuncionarioId;
                    comissao.ServicoId = servico.ServicoId;
                    comissao.valorComissao = (servico.Servico.Preco * 5) / 100;
                    comissao.RegistroCompraFiadaId = compraFiada.RegistroCompraFiadaId;

                    bdComissao.Adicionar(comissao);
                    bdComissao.SalvarTodos();
                }

            }


            Retorno.Sucesso = true;
            Retorno.LimparForm = true;
            Retorno.Redirecionar = true;
            Retorno.Link = "/Admin/CompraFiado/Index";

            return Json(Retorno, JsonRequestBehavior.AllowGet);
        }



        public ActionResult CreateProdutoFiado()
        {
            return View();
        }

        public class ProdutoValido
        {
            public int IdProduto { get; set; }
            public int Quant { get; set; }
        }

        [HttpPost]
        public JsonResult CreateProdutoFiado(RegistroCompraFiada compraFiada, FormCollection collection)
        {
            var Retorno = new RetornoJson();
            if (compraFiada.ClienteId == 0)
                Retorno.Mensagem += "<span>Selecionar o Cliente</span>";
            if (compraFiada.FuncionarioId == 0)
                Retorno.Mensagem += "<span>Selecionar o Funcionário</span>";
            // validar data para pagamento
            if (!Utilitarios.validarData(compraFiada.DataParaPagamento.ToString()))
                Retorno.Mensagem += "<span>Inserir uma Data</span>";
            if (compraFiada.DataParaPagamento <= DateTime.Now || compraFiada.DataParaPagamento > DateTime.Now.AddMonths(2))
                Retorno.Mensagem += "<span> Não aceitamos data anterior ou igual a data atual ou data acima de dois meses em relação a data que o servico foi prestado</span>";

            if (Retorno.Mensagem != "")
                return Json(Retorno, JsonRequestBehavior.AllowGet);

            if (collection["Produtos"] != null)
            {

                var Estoque = contexto.Produto.ToList();
                var Produtos = collection["Produtos"].Split(',');
                var ProdutosValido = new List<ProdutoValido>();


                foreach (var produto in Produtos)
                {
                    var produtoId = int.Parse(produto);
                    var Qtde = int.Parse(collection["Quantidade_" + produtoId]);
                    //validação importante que não permite valor negativo
                    if (Qtde < 0)
                    {
                        Retorno.Mensagem = "<span>Valor não pode ser Negativo</span>";
                        if (Retorno.Mensagem != "")
                            return Json(Retorno, JsonRequestBehavior.AllowGet);
                    }

                    if (Estoque.Any(x => x.ProdutoId == produtoId && x.Quantidade >= Qtde))
                    {
                        //cria a lista da compra de produto que tem quantidade em estoque para vender
                        ProdutosValido.Add(new ProdutoValido()
                        {
                            IdProduto = produtoId,
                            Quant = Qtde
                        });

                        //subtrair quantidade do produto abaixo que foi vendido atualizando a qtde
                        var ProdutoAtualizar = contexto.Produto.Where(x => x.ProdutoId == produtoId).FirstOrDefault();
                        ProdutoAtualizar.Quantidade = ProdutoAtualizar.Quantidade - Qtde;
                        var bdProduto = new ProdutoRepositorioEF(contexto);
                        bdProduto.Atualizar(ProdutoAtualizar);
                        bdProduto.SalvarTodos();

                    }//verifica se tem no estoque
                    else
                    {
                        Retorno.Mensagem += "<span>Sua compra e maior que a qtde em estoque</span>";
                    }
                }

                if (Retorno.Mensagem != "")
                    return Json(Retorno, JsonRequestBehavior.AllowGet);


                if (Retorno.Mensagem == "")
                {
                    // cria a compra fiada
                    compraFiada.DataCompra = DateTime.Now;
                    var registroCompraFiada = new RegistroCompraFiadaRepositorioEF(contexto);
                    registroCompraFiada.Adicionar(compraFiada);
                    registroCompraFiada.SalvarTodos();

                    // salva os produtos desta compra fiada
                    var bdProdutoFiado = new ProdutoFiadoRepositorioEF(contexto);
                    foreach (var produtoValido in ProdutosValido)
                    {
                        bdProdutoFiado.Adicionar(new ProdutoFiado()
                        {
                            RegistroCompraFiadaId = compraFiada.RegistroCompraFiadaId,
                            ProdutoId = produtoValido.IdProduto,
                            Quantidade = produtoValido.Quant

                        });
                    }
                    bdProdutoFiado.SalvarTodos();

                    Retorno.LimparForm = true;
                    Retorno.Sucesso = true;
                    Retorno.Redirecionar = true;
                    Retorno.Link = "/Admin/CompraFiado/Index";
                }

            }//valida se selecionou produto
            else
            {
                Retorno.Mensagem += "<span>Nenhum produto foi Selecionado</span>";
            }

            return Json(Retorno, JsonRequestBehavior.AllowGet);
        }

        // editando a compra de produtos fiado
        public PartialViewResult EditProdutoFiado(int id)
        {
            ViewBag.Clientes = contexto.Cliente.Where(x => x.Status.Nome.Equals("Ativo")).OrderBy(x => x.Nome).ToList();
            ViewBag.Funcionarios = contexto.Funcionario.Where(x => x.Status.Nome.Equals("Ativo")).OrderBy(x => x.Nome).ToList();
            ViewBag.Produtos = contexto.Produto.Where(x => x.Status.Nome.Equals("Ativo")).OrderBy(x => x.Nome).ToList();
            ViewBag.Quantidade = contexto.ProdutoFiado.Where(x => x.RegistroCompraFiadaId == id).ToList();

            var ProdutoFiado = contexto.RegistroCompraFiada.Where(x => x.RegistroCompraFiadaId == id).FirstOrDefault();
            return PartialView(ProdutoFiado);
        }
        // post de editar compra de produto fiado
        [HttpPost]
        public JsonResult EditProdutoFiado(RegistroCompraFiada compraFiada, FormCollection collection)
        {
            var Retorno = new RetornoJson();

            if (!UI.Web.Helpers.Utilitarios.validarData(compraFiada.DataParaPagamento.ToString()))
            {
                Retorno.Mensagem += "<span>Data invalida, inserir data correta</span>";
            }
            if (compraFiada.DataParaPagamento <= compraFiada.DataCompra || compraFiada.DataParaPagamento > compraFiada.DataCompra.AddMonths(2))
                Retorno.Mensagem += "<span> Não aceitamos data anterior a data da compra ou data para pagamento maior que dois meses em relação a compra</span>";
            if (Retorno.Mensagem != "")
                return Json(Retorno, JsonRequestBehavior.AllowGet);

            var bdRegistroCompraFiada = new RegistroCompraFiadaRepositorioEF(contexto);
            bdRegistroCompraFiada.Atualizar(compraFiada);
            bdRegistroCompraFiada.SalvarTodos();


            var ProdutosSalvosBancoDados = contexto.ProdutoFiado.Where(x => x.RegistroCompraFiadaId == compraFiada.RegistroCompraFiadaId).ToList();
            var bdProduto = new ProdutoRepositorioEF(contexto);
            var bdProdutoFiado = new ProdutoFiadoRepositorioEF(contexto);
            var Estoque = contexto.Produto.ToList();

            if (collection["Produtos"] == null)
            {
                Retorno.Mensagem += "<span> Não foi selecionado nenhum Produto!<span>";
                if (Retorno.Mensagem != "")
                    return Json(Retorno, JsonRequestBehavior.AllowGet);
                //// devolve para o estoque os produtos da compra fiada
                //foreach (var produtoSalvoBancoDados in ProdutosSalvosBancoDados)
                //{
                //    var Produto = contexto.Produto.Where(x => x.ProdutoId == produtoSalvoBancoDados.ProdutoId).FirstOrDefault();
                //    Produto.Quantidade = Produto.Quantidade + produtoSalvoBancoDados.Quantidade;
                //    bdProduto.Atualizar(Produto);
                //}
                //bdProduto.SalvarTodos();

                ////exclui a compra de produto fiado
                //var ProdutosFiado = contexto.ProdutoFiado.Where(x => x.RegistroCompraFiadaId == compraFiada.RegistroCompraFiadaId).ToList();
                //foreach (var produtoFiado in ProdutosFiado)
                //{
                //    bdProdutoFiado.Excluir(x => x.RegistroCompraFiadaId == produtoFiado.RegistroCompraFiadaId && x.ProdutoId == produtoFiado.ProdutoId);
                //}
                //bdProdutoFiado.SalvarTodos();

                //Retorno.Sucesso = true;
                //Retorno.LimparForm = true;
                //Retorno.Redirecionar = true;
                //Retorno.Link = "/Admin/CompraFiado/Index";
            }
            else
            {
                var Produtos = collection["Produtos"].Split(',');

                // se o poduto tiver no banco mas nao tiver na lista
                foreach (var produtoBanco in ProdutosSalvosBancoDados)
                {
                    if (!Produtos.Any(x => x == produtoBanco.ProdutoId.ToString()))
                    {
                        var ProdutoAtualizar = contexto.Produto.Where(x => x.ProdutoId == produtoBanco.ProdutoId).FirstOrDefault();
                        //devolver para o estoque o produto excluido da compra
                        ProdutoAtualizar.Quantidade = ProdutoAtualizar.Quantidade + produtoBanco.Quantidade;
                        bdProduto.Atualizar(ProdutoAtualizar);
                        bdProduto.SalvarTodos();
                        // excluir o produto que não foi selecionado na edição
                        bdProdutoFiado.Excluir(x => x.RegistroCompraFiadaId == compraFiada.RegistroCompraFiadaId
                        && x.ProdutoId == produtoBanco.ProdutoId);
                        bdProdutoFiado.SalvarTodos();
                    }
                }

                foreach (var produto in Produtos)
                {
                    var idProduto = int.Parse(produto);
                    var qtde = int.Parse(collection["Quantidade_" + idProduto]);
                    //validação importante que não permite valor negativo
                    if (qtde < 0)
                    {
                        Retorno.Mensagem = "<span>Valor não pode ser Negativo</span>";
                        if (Retorno.Mensagem != "")
                            return Json(Retorno, JsonRequestBehavior.AllowGet);
                    }
                    var Produto = contexto.Produto.Where(x => x.ProdutoId == idProduto).FirstOrDefault();

                    // se na lista tiver e no banco não tiver salva o que ta na lista no banco
                    if (!ProdutosSalvosBancoDados.Any(x => x.ProdutoId == idProduto) || ProdutosSalvosBancoDados == null)
                    {
                        //verifica se tem no estoque e subtrai a quantidade vendida
                        if (Estoque.Any(x => x.ProdutoId == idProduto && x.Quantidade >= qtde))
                        {
                            Produto.Quantidade = Produto.Quantidade - qtde;
                            bdProduto.Atualizar(Produto);
                            bdProduto.SalvarTodos();
                            // adiciona o produto na compra a prazo
                            bdProdutoFiado.Adicionar(new ProdutoFiado()
                            {
                                RegistroCompraFiadaId = compraFiada.RegistroCompraFiadaId,
                                ProdutoId = idProduto,
                                Quantidade = qtde

                            });
                            bdProdutoFiado.SalvarTodos();
                        }
                        else
                        {
                            Retorno.Mensagem += "<span>Produto não contem no estoque ou sua compra é maior do que tem disponivel</span>";
                        }
                        if (Retorno.Mensagem != "")
                            return Json(Retorno, JsonRequestBehavior.AllowGet);
                    }
                    //se tiver na lista e tiver no banco mas quantidade é diferente
                    if (ProdutosSalvosBancoDados.Any(x => x.ProdutoId == idProduto && x.Quantidade != qtde))
                    {
                        var ProdutoFiadoAtualizar = contexto.ProdutoFiado.Where(x => x.ProdutoId == idProduto && x.RegistroCompraFiadaId == compraFiada.RegistroCompraFiadaId).FirstOrDefault();
                        int qtdeAtual = ProdutosSalvosBancoDados.Where(x => x.ProdutoId == idProduto).FirstOrDefault().Quantidade;
                        int diferenca;

                        if (qtdeAtual > qtde)
                        {
                            diferenca = qtdeAtual - qtde;
                            //adicionar a diferenca para o estoque de produto e salvar nova quantidade
                            Produto.Quantidade = Produto.Quantidade + diferenca;
                            bdProduto.Atualizar(Produto);
                            bdProduto.SalvarTodos();
                            //altera a quantidade na compra produto fiado
                            ProdutoFiadoAtualizar.Quantidade = qtde;
                            bdProdutoFiado.Atualizar(ProdutoFiadoAtualizar);
                            bdProdutoFiado.SalvarTodos();

                        }
                        else
                        {
                            diferenca = qtde - qtdeAtual;
                            //verificar se tem no estoque
                            if (Estoque.Any(x => x.ProdutoId == idProduto && x.Quantidade >= diferenca))
                            {
                                //subtrair a quantidade comprada do estoque
                                Produto.Quantidade = Produto.Quantidade - diferenca;
                                bdProduto.Atualizar(Produto);
                                bdProduto.SalvarTodos();
                                //alterar quantidade na compra produto fiado
                                ProdutoFiadoAtualizar.Quantidade = qtde;
                                bdProdutoFiado.Atualizar(ProdutoFiadoAtualizar);
                                bdProdutoFiado.SalvarTodos();

                            }
                            else
                            {
                                Retorno.Mensagem += "<span>Sua alteração excede a quantidade disponivel</span>";
                            }
                            if (Retorno.Mensagem != "")
                                return Json(Retorno, JsonRequestBehavior.AllowGet);
                        }
                    }

                }
            }
            Retorno.Sucesso = true;
            Retorno.LimparForm = true;
            Retorno.Redirecionar = true;
            Retorno.Link = "/Admin/CompraFiado/Index";
            return Json(Retorno, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult Delete(int id)
        //{
        //    var CompraFiada = contexto.RegistroCompraFiada.Where(x => x.RegistroCompraFiadaId == id).FirstOrDefault();
        //    var bdRegistroCompraFiada = new RegistroCompraFiadaRepositorioEF(contexto);
        //    bdRegistroCompraFiada.Excluir(x => x.RegistroCompraFiadaId == id);
        //    bdRegistroCompraFiada.SalvarTodos();

        //    return RedirectToAction("Index");

        //}

    }
}