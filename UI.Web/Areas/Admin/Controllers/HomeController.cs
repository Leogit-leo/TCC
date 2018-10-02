using Aplicacao.Core.Dominio;
using Aplicacao.Core.RepositorioEF;
using Aplicacao.Core.RepositorioEF.Contexto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using UI.Web.Attributes;

namespace UI.Web.Areas.Admin.Controllers
{

    [CustomAuthorize]
    public class HomeController : Controller
    {
        BancoContexto contexto;
        Funcionario funcionarioLogado;

        public HomeController()
        {
            contexto = new BancoContexto();
            funcionarioLogado = UI.Web.Helpers.UserLogin.GetUsuarioAdmin();
        }

        [OnlyAdminAttibute]
        public ActionResult Index(int? mes , int? ano)
        {
            var EsteMes = DateTime.Now.Month;
            ViewBag.MesSelecionado = mes;
            if (mes == null)
            {
                mes = EsteMes;
                ViewBag.MesSelecionado = EsteMes;
            }

            var EsteAno = DateTime.Now.Year;
            ViewBag.AnoSelecionado = ano;
            if(ano == null)
            {
                ViewBag.AnoSelecionado = EsteAno;
                ano = EsteAno;
            }

            //comissoes dos funcionarios
            List<Comissao> ComissoesFuncionario = contexto.Comissao
            .Where(x => x.Data.Month == mes && x.Data.Year == ano)
            .ToList().GroupBy(x => x.FuncionarioId).Select(sel => new Comissao
            {
                FuncionarioId = sel.First().FuncionarioId,
                Funcionario = sel.Select(fun=> new Funcionario
                {
                    Nome = sel.First().Funcionario.Nome
                }).First(),
                valorComissao = sel.Sum(soma=> soma.valorComissao)

            }).ToList();

            ViewBag.ComissaoFuncionario = ComissoesFuncionario;

            // produtos mais vendidos no mes com pagamento á vista
            List<ItemCompra> Lista = contexto.ItemCompra
            .Where(x => x.RegistroCompra.DataCompra.Month == mes && x.RegistroCompra.DataCompra.Year == ano)
            .ToList()
            .GroupBy(l => l.ProdutoId)
            .Select(cl => new ItemCompra
            {
                ProdutoId = cl.First().ProdutoId,
                Quantidade = cl.Sum(c => c.Quantidade),
                Produto = cl.First().Produto
            }).ToList();

            ViewBag.ProdutoMesAVista = Lista;

            // servicos do mês que foram realizados e pagos
            List<Agenda> servicosDiaPago = contexto.Agenda.Where(x => x.Inicio.Month == mes && x.Inicio.Year == ano && x.Pago == true).ToList()
                .GroupBy(s => s.ServicoId)
                .Select(sel => new Agenda
                {
                    ServicoId = sel.Count(),
                    Servico = sel.Select(serv => new Servico
                    {
                        Nomeservico = sel.First().Servico.Nomeservico,
                        Preco = sel.Sum(s => s.Servico.Preco),
                        TempoGasto = sel.Sum(s => s.Servico.TempoGasto),

                    }).First(),

                }).ToList();

            ViewBag.ServicosDiaPago = servicosDiaPago;

            //servicos do mês realizados fiado 

            List<ServicoFiado> servicoMesFiado = contexto.ServicoFiado
                .Where(x => x.RegistroCompraFiada.DataCompra.Month == mes && x.RegistroCompraFiada.DataCompra.Year == ano)
                .ToList()
                .GroupBy(x => x.ServicoId)
                .Select(sel => new ServicoFiado
                {
                    ServicoFiadoId = sel.Count(),
                    Servico = sel.Select(serv => new Servico
                    {
                        Nomeservico = sel.First().Servico.Nomeservico,
                        Preco = sel.Sum(soma => soma.Servico.Preco),
                        TempoGasto = sel.Sum(soma => soma.Servico.TempoGasto),

                    }).First()

                }).ToList();


            ViewBag.ServicoMesFiado = servicoMesFiado;


            // Produtos vendido no mês com pagamento fiado
           

            List<ProdutoFiado> ProdutosMesPagamentoFiado = contexto.ProdutoFiado
                .Where(x => x.RegistroCompraFiada.DataCompra.Month == mes && x.RegistroCompraFiada.DataCompra.Year == ano)
                .ToList()
                .GroupBy(x => x.ProdutoId).Select(sel => new ProdutoFiado
                {
                    ProdutoId = sel.First().ProdutoId,
                    Quantidade = sel.Sum(soma => soma.Quantidade),
                    Produto = sel.Select(s => new Produto
                    {
                        Nome = s.Produto.Nome,
                        Preco = sel.Sum(soma => soma.Quantidade * soma.Produto.Preco)
                    }).First(),

                }).ToList();

            ViewBag.ProdutosMesPagamentoFiado = ProdutosMesPagamentoFiado;


            // Horas por Mês de trabalho de cada funcionario em relação servicos fiado

            List<RegistroCompraFiada> horasFuncionarioServicoFiado = contexto.RegistroCompraFiada
                .Where(x => x.DataCompra.Month == mes && x.DataCompra.Year == ano)
                .ToList()
                .GroupBy(x => x.FuncionarioId).Select(sel => new RegistroCompraFiada
                {
                    FuncionarioId = sel.First().FuncionarioId,
                    Funcionario = sel.Select(f => new Funcionario
                    {
                        Nome = f.Funcionario.Nome
                    }).First(),




                }).ToList();

            ViewBag.FuncionarioHoraServicoFiado = horasFuncionarioServicoFiado;

            // Horas por mes de trabalho de cada funcionario em relação servicos pagos

            List<Agenda> HoraMesFuncionarioServicoPago = contexto.Agenda
                .Where(x => x.Inicio.Month == mes && x.Pago && x.Inicio.Year == ano)
                .ToList()
                .GroupBy(x => x.FuncionarioId).Select(sel => new Agenda
                {
                    Funcionario = sel.Select(fun => new Funcionario
                    {
                        Nome = fun.Funcionario.Nome
                    }).First(),

                    Servico = sel.Select(ser => new Servico
                    {
                        TempoGasto = sel.Sum(soma => soma.Servico.TempoGasto)
                    }).First(),

                    TempoGasto = sel.Sum(x => x.Fim.Hour - x.Inicio.Hour)

                }).ToList();

            ViewBag.FuncionarioServicoMesPago = HoraMesFuncionarioServicoPago;

            return View();
        }
       
        public ActionResult MeuPerfil(int id)
        {
            ViewBag.Servicos = contexto.Servico.OrderBy(x => x.ServicoId).ToList();
            ViewBag.Funcionarios = contexto.Funcionario.Where(x=> x.FuncionarioId != funcionarioLogado.FuncionarioId).OrderBy(x => x.FuncionarioId).ToList();

            var perfil = contexto.Funcionario.Where(x => x.FuncionarioId == id).FirstOrDefault();
            return View(perfil);
        }

        
        [HttpPost]
        public JsonResult CreateNotificacao(Notificacao Notificacao)
        {
            var Retorno = new RetornoJson();

            if (Notificacao.Titulo == null)
                Retorno.Mensagem += "<span> Inserir Titulo</span>";
            if (Notificacao.Mensagem == null)
                Retorno.Mensagem += "<span> Digite a Mensagem</span>";
            if (Retorno.Mensagem != "")
                return Json(Retorno, JsonRequestBehavior.AllowGet);
            try
            {
                if (Notificacao.FuncionarioDestino == 0)
                    Notificacao.FuncionarioDestino = null;

                Notificacao.DataNotificacao = DateTime.Now;
                Notificacao.FuncionarioId = funcionarioLogado.FuncionarioId;

                var bdNotificacao = new NotificacaoRepositorioEF(contexto);
                bdNotificacao.Adicionar(Notificacao);
                bdNotificacao.SalvarTodos();

                Retorno.Mensagem += "<span> Notificação salva com sucesso</span>";
                Retorno.LimparForm = true;
                Retorno.Sucesso = true;
            }
            catch (Exception e)
            {
                Retorno.Mensagem += "<span> Cadastrado nao foi completado</span>";
            }
            return Json(Retorno, JsonRequestBehavior.AllowGet);

        }

        
        public void SalvarNotificacaoVisualizada(int notificacaoId)
        {
            var ExisteNotificacao = contexto.RelFuncionarioNotificacao.Where(x => x.NotificacaoId == notificacaoId &&
            x.FuncionarioId == funcionarioLogado.FuncionarioId).FirstOrDefault();

            if (ExisteNotificacao == null)
            {

                DateTime data = DateTime.Now;
                int funcionarioID = funcionarioLogado.FuncionarioId;

                var bdRelFuncionarioNotificacao = new RelFuncionarioNotificacaoRepositorioEF(contexto);
                bdRelFuncionarioNotificacao.Adicionar(new RelFuncionarioNotificacao()
                {
                    FuncionarioId = funcionarioID,
                    NotificacaoId = notificacaoId,
                    DataVisualizacao = data
                });

                bdRelFuncionarioNotificacao.SalvarTodos();

            }
        }

        public PartialViewResult TodasNotificacoes()
        {
            var MinhasNotificacoes = contexto.Notificacao.Where(x => x.FuncionarioId != funcionarioLogado.FuncionarioId
          && (x.FuncionarioDestino == null || x.FuncionarioDestino == funcionarioLogado.FuncionarioId))
          .OrderByDescending(x=> x.DataNotificacao)
          .ToList();
            return PartialView(MinhasNotificacoes);
        }

        public ActionResult AcessoNegado()
        {
            return View();
        }
    }
}
