using Aplicacao.Core.Dominio;
using Aplicacao.Core.RepositorioEF;
using Aplicacao.Core.RepositorioEF.Contexto;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    public class AgendaController : Controller
    {
        BancoContexto contexto;
        Funcionario usuarioLogado;

        public AgendaController()
        {
            contexto = new BancoContexto();
            ViewBag.Controller = "Agenda";
            ViewBag.TitulosForm = "Agenda";
            ViewBag.tblDinamico = "tblAgendas";

            usuarioLogado = UI.Web.Helpers.UserLogin.GetUsuarioAdmin();

            ViewBag.Funcionarios = contexto.Funcionario.OrderBy(x => x.FuncionarioId).ToList();

            ViewBag.Clientes = contexto.Cliente.Where(x=> x.Status.Nome.Equals("Ativo")).OrderBy(x => x.ClienteId).ToList();

            ViewBag.Servicos = contexto.Servico.Where(x=> x.Status.Nome.Equals("Ativo")).OrderBy(x => x.ServicoId).ToList();

            ViewBag.FuncionarioServico = contexto.RelFuncionarioServico.ToList();


        }

        public ActionResult Index(int? page, string s)
        {
            s = (s != null) ? s.ToLower() : null;

            var Agendamentos = contexto.Agenda.Where(x => x.Cliente.Nome.ToLower().Contains(s) || x.Funcionario.Nome.ToLower().Contains(s) || x.Servico.Nomeservico.ToLower().Contains(s) || s == null).OrderByDescending(x => x.AgendaId).ToPagedList(page ?? 1, 10);
            return View(Agendamentos);
        }


        public ActionResult IndexAgenda()
        {
            ViewBag.Clientes = contexto.Cliente.Where(x=> x.Status.Nome.Equals("Ativo")).OrderBy(x => x.ClienteId).ToList();
            ViewBag.Funcionarios = contexto.Funcionario.OrderBy(x => x.FuncionarioId).ToList();
            return View();
        }

        public PartialViewResult LoadServicosEfuncionarios(int id , int? ServicoID)
        {
            ViewBag.Servico = ServicoID;
            var FuncionarioSelecionado = contexto.Funcionario.Where(x => x.FuncionarioId == id).FirstOrDefault();
            return PartialView(FuncionarioSelecionado);
        }


        public PartialViewResult LoadServicosFuncionario(int id, int? ServicoID)
        {
            ViewBag.Servico = ServicoID;
            var FuncionarioSelecionado = contexto.Funcionario.Where(x => x.FuncionarioId == id).FirstOrDefault();
            return PartialView(FuncionarioSelecionado);
        }

        [HttpPost]
        public JsonResult Create(Agenda Agenda)
        {
            var Retorno = new RetornoJson();

            if (Agenda.FuncionarioId == 0)
                Retorno.Mensagem += "<span> Escolha o Funcionario</span>";
            if (Agenda.ClienteId == 0)
                Retorno.Mensagem += "<span> Escolha o Cliente</span>";
            if (Agenda.ServicoId == 0)
                Retorno.Mensagem += "<span> Escolha o Servico</span>";

            if (Retorno.Mensagem != "")
                return Json(Retorno, JsonRequestBehavior.AllowGet);

            TimeSpan minuto = new TimeSpan(00, 30, 00);


            DateTime DataAgenda = DateTime.Parse(Agenda.DataAgendamento.ToString());
            Agenda.Inicio = DataAgenda + Agenda.Inicio.TimeOfDay;
            Agenda.Fim = DataAgenda + Agenda.Fim.TimeOfDay;

            //validar se o horario Inicio é menor que o Fim
            if(Agenda.Inicio >= Agenda.Fim)
                Retorno.Mensagem += "<span> Horario Inicio é obrigatoriamente menor que o horário Fim</span>";

            //validar se a data ultrapassa 4 meses
            DateTime AgendamentoDistante = DateTime.Now.AddMonths(4);
            if (Agenda.Fim > AgendamentoDistante)
                Retorno.Mensagem += "<span> Agendamento não pode ultrapassar 4 meses</span>";

            //agendamentos somente devem ser feito em horario de funcionamento
            TimeSpan HorarioAbre = new TimeSpan(10, 00, 00);
            TimeSpan HorarioFecha = new TimeSpan(20, 00, 00);
            if(!(Agenda.Inicio.Hour >= HorarioAbre.Hours && Agenda.Inicio.Hour <= HorarioFecha.Hours) || !(Agenda.Fim.Hour >= HorarioAbre.Hours && Agenda.Fim.Hour <= HorarioFecha.Hours))
                Retorno.Mensagem += "<span> O Agendamento deve ser dentro do horário de funcionamento do estabelecimento</span>";
            

            //todo agendamento deve ser agendado com 30 minutos de antecedencia
            DateTime DataHoraRetroativa = Agenda.Inicio.Subtract(minuto);
            if (DataHoraRetroativa <= DateTime.Now)
                Retorno.Mensagem += "<span> Não é possivel agendar nesta data, pois todos agendamentos precisam ter pelo menos 30 minutos de antecedencia</span>";

            //nao pode agendar em horario igual com o mesmo funcionario
            var agendaMarcada = contexto.Agenda.Where(x => x.FuncionarioId == Agenda.FuncionarioId).Count(x => (Agenda.Inicio >= x.Inicio && Agenda.Inicio <= x.Fim) || (Agenda.Fim >= x.Inicio && Agenda.Fim <= x.Fim)) > 0; 
            if(agendaMarcada)
                Retorno.Mensagem += "<span> Escolha outro horario</span>";
          

            if (Retorno.Mensagem != "")
                return Json(Retorno, JsonRequestBehavior.AllowGet);

            try
            {
                var bdAgenda = new AgendaRepositorioEF(contexto);
                bdAgenda.Adicionar(Agenda);
                bdAgenda.SalvarTodos();

                //tratando as comissoes
                if(Agenda.Pago == true)
                {
                    using (var bdComisao = new ComissaoRepositorioEF(contexto))
                    {
                        Comissao comissao = new Comissao();
                        comissao.Data = DateTime.Now;
                        comissao.AgendaId = Agenda.AgendaId;
                        comissao.FuncionarioId = Agenda.FuncionarioId;
                        comissao.ServicoId = Agenda.ServicoId;
                        comissao.valorComissao = (Agenda.Servico.Preco * 5) /100;

                        bdComisao.Adicionar(comissao);
                        bdComisao.SalvarTodos();
                    }
                }
               
                Retorno.Sucesso = true;
                Retorno.Mensagem += "<span> Cadastrado com sucesso</span>";
                Retorno.Redirecionar = true;
                Retorno.Link = "/Admin/Agenda/IndexAgenda";
            }
            catch (Exception e)
            {
                Retorno.Mensagem += "<span> Não foi possivel persistir seu agendamento</span>";
            }
            return Json(Retorno, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public JsonResult Edit(Agenda Agenda)
        {
            var Retorno = new RetornoJson();
            TimeSpan minuto = new TimeSpan(00, 30, 00);

            DateTime DataAgenda = DateTime.Parse(Agenda.DataAgendamento.ToString());
            Agenda.Inicio = DataAgenda + Agenda.Inicio.TimeOfDay;
            Agenda.Fim = DataAgenda + Agenda.Fim.TimeOfDay;

            //validar se o horario Inicio é menor que o Fim
            if (Agenda.Inicio >= Agenda.Fim)
                Retorno.Mensagem += "<span> Horario Inicio é obrigatoriamente menor que o horário Fim</span>";

            //validar se a data ultrapassa 4 meses
            DateTime AgendamentoDistante = DateTime.Now.AddMonths(4);
            if (Agenda.Fim > AgendamentoDistante)
                Retorno.Mensagem += "<span> Agendamento não pode ultrapassar 4 meses</span>";

            //agendamentos somente devem ser feito em horario de funcionamento
            TimeSpan HorarioAbre = new TimeSpan(10, 00, 00);
            TimeSpan HorarioFecha = new TimeSpan(20, 00, 00);
            if (!(Agenda.Inicio.Hour >= HorarioAbre.Hours && Agenda.Inicio.Hour <= HorarioFecha.Hours) || !(Agenda.Fim.Hour >= HorarioAbre.Hours && Agenda.Fim.Hour <= HorarioFecha.Hours))
                Retorno.Mensagem += "<span> O Agendamento deve ser dentro do horário de funcionamento do estabelecimento</span>";


            //todo agendamento deve ser agendado com 30 minutos de antecedencia
            DateTime DataHoraRetroativa = Agenda.Inicio.Subtract(minuto);
            if (DataHoraRetroativa <= DateTime.Now)
                Retorno.Mensagem += "<span> Não é possivel agendar nesta data, pois todos agendamentos precisam ter pelo menos 30 minutos de antecedencia</span>";

            //nao pode agendar em horario igual com o mesmo funcionario mas como esta editando ele aceita o mesmo horario ainda que foi criado o agendamento inicial
            var EditandoMesmaAgendamento = contexto.Agenda.Any(x => x.FuncionarioId == Agenda.FuncionarioId
                && x.Inicio == Agenda.Inicio && x.Fim == Agenda.Fim && x.ClienteId == Agenda.ClienteId
                );

            if (!EditandoMesmaAgendamento)
            {
                var agendaMarcada = contexto.Agenda.Where(x => x.FuncionarioId == Agenda.FuncionarioId).Count(x => (Agenda.Inicio >= x.Inicio && Agenda.Inicio <= x.Fim) || (Agenda.Fim >= x.Inicio && Agenda.Fim <= x.Fim)) > 0;
                if (agendaMarcada)
                    Retorno.Mensagem += "<span> Escolha outro horario</span>";
            }
           

            if (Retorno.Mensagem != "")
                return Json(Retorno, JsonRequestBehavior.AllowGet);


            var bdAgenda = new AgendaRepositorioEF(contexto);
            bdAgenda.Atualizar(Agenda);
            bdAgenda.SalvarTodos();

            //abaixo tratando as comissoes
            if(Agenda.Pago == false)
            {
                var Existecomissao = contexto.Comissao.FirstOrDefault(x => x.AgendaId == Agenda.AgendaId);
                if (Existecomissao != null)
                {
                    //se o usuario setar como nao pago o agendamento exclui a comissao
                    using (var bdComisao = new ComissaoRepositorioEF(contexto))
                    {
                        var comissao = contexto.Comissao.FirstOrDefault(x => x.AgendaId == Agenda.AgendaId);
                        bdComisao.Excluir(x => x.AgendaId == Agenda.AgendaId);
                        bdComisao.SalvarTodos();
                    }
                }
            }
            //se o usuario editar  e deixar agenda paga, edita a comissao
            if (Agenda.Pago == true)
            {
                using (var bdComisao = new ComissaoRepositorioEF(contexto))
                {
                    var comissao = contexto.Comissao.FirstOrDefault(x => x.AgendaId == Agenda.AgendaId);

                    //no caso do usuario ter excluido a comissao cria ela denovo
                    if (comissao == null)
                    {
                        {
                            Comissao c = new Comissao();
                            c.Data = DateTime.Now;
                            c.AgendaId = Agenda.AgendaId;
                            c.FuncionarioId = Agenda.FuncionarioId;
                            c.ServicoId = Agenda.ServicoId;
                            c.valorComissao = (Agenda.Servico.Preco * 5) / 100;

                            bdComisao.Adicionar(c);
                            bdComisao.SalvarTodos();
                        }
                    }
                    else // neste caso ele nao excluiu a comissao apenas editou a agenda e deixou agenda paga
                    {
                        comissao.Data = comissao.Data;
                        comissao.AgendaId = comissao.AgendaId;
                        comissao.FuncionarioId = Agenda.FuncionarioId;
                        comissao.ServicoId = Agenda.ServicoId;
                        comissao.valorComissao = (Agenda.Servico.Preco * 5) / 100;

                        bdComisao.Atualizar(comissao);
                        bdComisao.SalvarTodos();
                    }
                }
            }

            Retorno.Mensagem += "<span> Editado com sucesso</span>";

            Retorno.Sucesso = true;
            Retorno.Redirecionar = true;
            Retorno.Link = "/Admin/Agenda/IndexAgenda";

            return Json(Retorno, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int id)
        {
            var ComissaoAgendaExcluir = contexto.Comissao.FirstOrDefault(x => x.AgendaId == id);
            if(ComissaoAgendaExcluir != null)
            {
                var bdComisao = new ComissaoRepositorioEF(contexto);
                
                    bdComisao.Excluir(x => x.AgendaId == id);
                    bdComisao.SalvarTodos();
                
            }
            var bdAgenda = new AgendaRepositorioEF(contexto);
            bdAgenda.Excluir(x => x.AgendaId == id);
            bdAgenda.SalvarTodos();
            return RedirectToAction("IndexAgenda");
        }

        //para enviar notificacao 19-11-17
        public void NotificarClienteApp(int id)
        {
            var Agenda = contexto.Agenda.FirstOrDefault(x => x.AgendaId == id && x.Pago == false);

            if(Agenda != null)
            {
                Notificacao notificacao = new Notificacao();
                notificacao.DataNotificacao = DateTime.Now;
                notificacao.Titulo = "Cancelamos seu Agendamento";
                notificacao.FuncionarioId = usuarioLogado.FuncionarioId;
                notificacao.ClienteId = Agenda.ClienteId;
                notificacao.Mensagem = "Caro Cliente estamos avisando que seu agendamento foi cancelado"+
                    "  Nome:" + Agenda.Cliente.Nome + "  Funcionario:"+ Agenda.Funcionario.Nome + "  Inicio:"+ Agenda.Inicio +
                    "  Fim:"+ Agenda.Fim +  
                    "  mas você pode solicitar um novo agendamento ou entre em contato conosco";

                var bdNotificacao = new NotificacaoRepositorioEF(contexto);
                bdNotificacao.Adicionar(notificacao);
                bdNotificacao.SalvarTodos();
            }
            //para enviar notificacao ao cliente que seu agendamento foi deletado , esse metodo apenas salva a notificacao
            //depois o cliente atravez da api puxa do banco de dados estas informações
        }


        // mostra todos agendamentos na agenda da view

        public JsonResult HorarioAgendado(DateTime start, DateTime end , int id)
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
                    dataagendamento = agenda.Inicio.ToString("dd/MM/yyyy"),
                    servicoPago = agenda.Pago,
                    servicoSelecionado = agenda.Servico.ServicoId
                });
            }

            return Json(listaAgenda, JsonRequestBehavior.AllowGet);

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
        public int clienteSelecionado { get; set; }
        public string dataagendamento { get; set; }
        public bool servicoPago { get; set; }
        public int servicoSelecionado { get; set; }

    }
}