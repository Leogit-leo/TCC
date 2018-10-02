using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace UI.Web.Areas.Admin.API
{
    public class AgendamentoController : ApiController
    {


        [HttpGet]
        public JToken agendamento_get_01(int id_agendamento)
        {
            List<AgendaAPI> Agendamento = new List<AgendaAPI>();
            Hashtable retorno = new Hashtable();
            string conexao = Properties.Settings.Default.string_conexao;

            try
            {
                using (ConectarBD con = new ConectarBD(conexao))
                {
                    //criar proc dos agendamentos
                    Agendamento = con.ExecuteQuery<AgendaAPI>("[dbo].[agendamento_get] {0}",
                                                                           id_agendamento
                               ).ToList();

                    retorno.Add("Retorno:", Agendamento);

                    con.Connection.Close();
                    con.Connection.Dispose();
                }

            }
            catch (Exception ex)
            {
                retorno.Add("Erro:", ex.Message);
            }

            return JObject.Parse(JsonConvert.SerializeObject(retorno));
        }




        [HttpGet]
        public JToken agendamento_get(int id_agendamento)
        {
            List<AgendaAPI> Agendamento = new List<AgendaAPI>();

            Hashtable retorno = new Hashtable();
            string conexao = Properties.Settings.Default.string_conexao;

            try
            {
                using (ConectarBD con = new ConectarBD(conexao))
                {
                    Agendamento = (

                         from ag in (con.ExecuteQuery<AgendaAPI>("[dbo].[agendamento_get] {0}", id_agendamento).AsEnumerable())

                         select new AgendaAPI
                         {
                             AgendaId = ag.AgendaId,
                             Pago = ag.Pago,
                             ClienteId = ag.ClienteId,
                             FuncionarioId = ag.FuncionarioId,
                             ServicoId = ag.ServicoId,
                             Inicio = ag.Inicio,
                             Fim = ag.Fim,

                              funcionario = (

                                       from fun in (con.ExecuteQuery<FuncionarioAPI>("[dbo].[funcionario_get] {0}", ag.FuncionarioId))

                                       select new FuncionarioAPI
                                       {
                                           FuncionarioId = fun.FuncionarioId,
                                           Nome = fun.Nome,
                                           Endereco = fun.Endereco,
                                           Telefone = fun.Telefone,
                                           Salario = fun.Salario,
                                           Email = fun.Email,
                                           Tipo = fun.Tipo,
                                           StatusId = fun.StatusId
                                       }
                             ).FirstOrDefault(),

                             cliente = (
                                    from c in (con.ExecuteQuery<ClienteAPI>("[dbo].[cliente_get] {0}", ag.ClienteId).ToList())

                                            select new ClienteAPI
                                            {
                                                ClienteId = c.ClienteId,
                                                Nome = c.Nome,
                                                Endereco = c.Endereco,
                                                Telefone = c.Telefone,
                                                Email = c.Email,
                                                CPF = c.CPF,
                                                StatusId = c.StatusId
                                            }

                             ).FirstOrDefault(),

                             servico = (
                                    from s in (con.ExecuteQuery<ServicoAPI>("[dbo].[servico_get] {0}", ag.ServicoId).ToList())

                                    select new ServicoAPI
                                    {
                                        ServicoId = s.ServicoId,
                                        Nomeservico = s.Nomeservico,
                                        Preco = s.Preco,
                                        TempoGasto = s.TempoGasto,
                                        StatusId = s.StatusId
                                    }

                             ).FirstOrDefault()
                         }

                    ).ToList();


                    retorno.Add("Retorno:", Agendamento);

                    con.Connection.Close();
                    con.Connection.Dispose();
                }

            }
            catch (Exception ex)
            {
                retorno.Add("Erro:", ex.Message);
            }

            return JObject.Parse(JsonConvert.SerializeObject(retorno));
        }

    }
}


