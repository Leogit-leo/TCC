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

        public ActionResult Index()
        {
            ViewBag.Titulo = "Home";

            var EsteMes = DateTime.Now.Month;

            //puxa os servicos pago que o funcinario fez atravez da agenda
            List<Agenda> ServicosDoFuncionario = contexto.Agenda
            .Where(x => x.Inicio.Month == EsteMes && x.FuncionarioId == funcionarioLogado.FuncionarioId && x.Pago == true)
            .ToList()
            .GroupBy(x => x.ServicoId).Select(sel => new Agenda
            {
                ServicoId = sel.Count(),
                Servico = sel.Select(ser=> new Servico
                {
                    Nomeservico = sel.First().Servico.Nomeservico,
                }).First()

            }).ToList();

            ViewBag.ServicosDoFuncionarioAgenda = ServicosDoFuncionario;
            //puxa os servicos que foram vendidos fiado e ja foram pagos que o funcionario do salao fez
            List<ServicoFiado> ServicoDoFuncionarioFiadoMasJaPago = contexto.ServicoFiado
            .Where(x => x.RegistroCompraFiada.FuncionarioId == funcionarioLogado.FuncionarioId && x.RegistroCompraFiada.Pago == true)
            .ToList()
            .GroupBy(x => x.ServicoId).Select(sel => new ServicoFiado
            {
                ServicoId = sel.Count(),
                Servico = sel.Select(ser => new Servico
                {
                    Nomeservico = sel.First().Servico.Nomeservico,
                }).First()

            }).ToList();

            ViewBag.ServicoDoFuncionarioFiadoMasJaPago = ServicoDoFuncionarioFiadoMasJaPago;

            // fazer a junção dos servicos do funcionario da agenda e do registro fiado mas que ambos estejam pagos


            return View();
        }


        public ActionResult Agenda()
        {
            ViewBag.Titulo = "Agenda";
            ViewBag.Clientes = contexto.Cliente.OrderBy(x => x.ClienteId).ToList();
            ViewBag.Funcionarios = contexto.Funcionario.OrderBy(x => x.Nome).ToList();
            return View();
        }


        public PartialViewResult LoadServicoFuncionario(int idFuncionario ,int? idServico)
        {
            ViewBag.Servico = idServico;
            var Funcionario = contexto.Funcionario.Where(x => x.FuncionarioId == idFuncionario).FirstOrDefault();
            return PartialView(Funcionario);
        }

        // mostra todos agendamentos na agenda da view

        public JsonResult HorarioAgendado(DateTime start, DateTime end, int id)
        {

            var listaAgenda = new List<fullcalendar>();

            var events = contexto.Agenda.Where(x => x.Inicio >= start && x.Fim <= end && x.FuncionarioId == id);

            foreach (var agenda in events)
            {
                listaAgenda.Add(new fullcalendar()
                {
                    id = agenda.AgendaId,
                    title = agenda.Cliente.Nome,
                    start = agenda.Inicio,
                    end = agenda.Fim,
                    color = "black",

                    funcionarioSelecionado = agenda.Funcionario.FuncionarioId,
                    clienteSelecionado = agenda.Cliente.ClienteId,
                    dataAgendamento = agenda.Inicio.ToString("dd/MM/yyyy"),
                    servicoSelecionado = agenda.Servico.ServicoId,

                    nomefuncionarioSelecionado = agenda.Funcionario.Nome,
                    nomeservicoSelecionado = agenda.Servico.Nomeservico,
                    nomeclienteSelecionado = agenda.Cliente.Nome,

                    servicoestapago = agenda.Pago.ToString()
                });
            }

            return Json(listaAgenda, JsonRequestBehavior.AllowGet);

        }

        public ActionResult MeuPerfil(int id)
        {
            ViewBag.Titulo = "Meu Perfil";
            ViewBag.Servicos = contexto.Servico.OrderBy(x => x.ServicoId).ToList();
            ViewBag.Funcionarios = contexto.Funcionario.Where(x=> x.FuncionarioId != funcionarioLogado.FuncionarioId).OrderBy(x => x.FuncionarioId).ToList();

            ViewBag.MinhasNotificacoes = contexto.Notificacao.Where(x => x.FuncionarioId != funcionarioLogado.FuncionarioId
            && (x.FuncionarioDestino == null || x.FuncionarioDestino == funcionarioLogado.FuncionarioId)).ToList();

            var perfil = contexto.Funcionario.Where(x => x.FuncionarioId == id).FirstOrDefault();
            return View(perfil);
        }

        [HttpPost]
        public JsonResult EditarMeuPerfilFuncionario(Funcionario funcionario, FormCollection colecaoServicos, string HiddenSenha, string HiddenConfSenha, HttpPostedFileBase file)
        {
            var Retorno = new RetornoJson();
            if (funcionario.Senha != funcionario.ConfirmarSenha)
                Retorno.Mensagem += "<span> As senhas devem ser identicas</span>";
            if (Retorno.Mensagem != "")
                return Json(Retorno, JsonRequestBehavior.AllowGet);

            if (funcionario.Senha == null && funcionario.ConfirmarSenha == null)
                funcionario.Senha = HiddenSenha;
            funcionario.ConfirmarSenha = HiddenConfSenha;

            if (file != null)
            {
                string nome = "Funcionario-" + NomeArquivo.GerarNomeArquivo(file.FileName);
                string path = Path.Combine(Server.MapPath(Caminho.Usuario()), Path.GetFileName(nome));
                file.SaveAs(path);
                funcionario.Foto = nome;
            }

            var listaServicosExistentes = contexto.RelFuncionarioServico.Where(x => x.FuncionarioId == funcionario.FuncionarioId).ToList();

            var bdRelFuncionarioServico = new RelFuncionarioServicoRepositorioEF(contexto);


            try
            {
                var bdFuncionario = new FuncionarioRepositorioEF(contexto);
                bdFuncionario.Atualizar(funcionario);
                bdFuncionario.SalvarTodos();


                // lista de servicos vazia
                if (colecaoServicos["Servico"] == null)
                {
                    bdRelFuncionarioServico.Excluir(x => x.FuncionarioId == funcionario.FuncionarioId);
                    bdRelFuncionarioServico.SalvarTodos();
                }

                if (colecaoServicos["Servico"] != null)
                {
                    var Servicos = colecaoServicos["Servico"].Split(',');
                    // se tiver na lista e nao tiver no banco de dados
                    foreach (var servico in Servicos)
                    {
                        var servicoId = int.Parse(servico);

                        if (!listaServicosExistentes.Any(x => x.ServicoId == servicoId))
                        {
                            bdRelFuncionarioServico.Adicionar(new RelFuncionarioServico()
                            {
                                FuncionarioId = funcionario.FuncionarioId,
                                ServicoId = servicoId
                            });
                        }
                        // se tiver no banco de dados mas nao conter na lista

                        foreach (var servicoExistente in listaServicosExistentes)
                        {
                            if (!Servicos.Any(x => x == servicoExistente.ServicoId.ToString()))
                            {
                                bdRelFuncionarioServico.Excluir(x => x.ServicoId == servicoExistente.ServicoId && x.FuncionarioId == funcionario.FuncionarioId);
                            }
                        }

                    }
                    bdRelFuncionarioServico.SalvarTodos();
                }



                Retorno.Mensagem += "<span> Atualizado com sucesso</span>";
                Retorno.Sucesso = true;
            }
            catch (Exception e)
            {
                Retorno.Mensagem += "<span> Erro ao editar seu perfil</span>";
            }

            return Json(Retorno, JsonRequestBehavior.AllowGet);

        }

        public JsonResult CriarNotificacao(Notificacao notificacao)
        {
            var Retorno = new RetornoJson();
            if (notificacao.Titulo == null)
                Retorno.Mensagem += "<span> Inserir o Titulo</span>";
            if (notificacao.Mensagem == null)
                Retorno.Mensagem += "<span> Inserir a Mensagem</span>";
            if (Retorno.Mensagem != "")
                return Json(Retorno, JsonRequestBehavior.AllowGet);
            try
            {
                notificacao.DataNotificacao = DateTime.Now;
                notificacao.FuncionarioId = funcionarioLogado.FuncionarioId;

                if (notificacao.FuncionarioDestino == 0)
                    notificacao.FuncionarioDestino = null;

                var bdNotificacao = new NotificacaoRepositorioEF(contexto);
                bdNotificacao.Adicionar(notificacao);
                bdNotificacao.SalvarTodos();

                Retorno.Mensagem += "<span> Enviada com sucesso !</span>";
                Retorno.LimparForm = true;
                Retorno.Sucesso = true;

            }
            catch (Exception e)
            {
                Retorno.Mensagem += "<span> Erro ao tentar enviar a Notificacao</span>";
            }

            return Json(Retorno, JsonRequestBehavior.AllowGet);

        }

        public void SalvarNotificacaoLida(int notificacaoId)
        {
            var NotificacaoExiste = contexto.RelFuncionarioNotificacao.Where(x => x.FuncionarioId == funcionarioLogado.FuncionarioId
            && x.NotificacaoId == notificacaoId).FirstOrDefault();

            if (NotificacaoExiste == null)
            {
                DateTime data = DateTime.Now;

                var bdRelFuncionarioNotificacao = new RelFuncionarioNotificacaoRepositorioEF(contexto);
                bdRelFuncionarioNotificacao.Adicionar(new RelFuncionarioNotificacao()
                {
                    FuncionarioId = funcionarioLogado.FuncionarioId,
                    NotificacaoId = notificacaoId,
                    DataVisualizacao = data

                });

                bdRelFuncionarioNotificacao.SalvarTodos();

            }
        }
    }

    public class fullcalendar
    {
        public int id { get; set; }
        public string title { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public string color { get; set; }

        public int funcionarioSelecionado { get; set; }
        public string nomefuncionarioSelecionado { get; set; }
        public int clienteSelecionado { get; set; }
        public string nomeclienteSelecionado { get; set; }
        public string dataAgendamento { get; set; }
        public int servicoSelecionado { get; set; }
        public string nomeservicoSelecionado { get; set; }

       public string servicoestapago { get; set; }

    }



}
