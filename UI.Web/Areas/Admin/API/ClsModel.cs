using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI.Web.Areas.Admin.API
{

    public class AgendaAPI
    {
      public int?  AgendaId { get; set; }
      public bool? Pago { get; set; }
      public int? ClienteId { get; set; }
      public int?  FuncionarioId { get; set; }
     
      public int ? ServicoId { get; set; }
      public DateTime? Inicio { get; set; }
      public DateTime?  Fim { get; set; }

      public FuncionarioAPI funcionario { get; set; }
      public ClienteAPI cliente { get; set; }
      public ServicoAPI servico { get; set; }

        public AgendaAPI()
        {
            funcionario = new FuncionarioAPI();
            cliente = new ClienteAPI();
            servico = new ServicoAPI();
        }
           
    }

    public class FuncionarioAPI
    {
       public int? FuncionarioId { get; set; }
       public string Nome { get; set; }
       public string Endereco { get; set; }
       public string Telefone { get; set; }
       public decimal? Salario { get; set; }
       
       public string Email { get; set; }
       public string Tipo { get; set; }
        
       public int? StatusId { get; set; }
    }

    public class ClienteAPI
    {
       public int ClienteId { get; set; }
       public string Nome { get; set; }
       public string Endereco { get; set; }
       public string Telefone { get; set; }
       public string Email { get; set; }
       public string CPF { get; set; }
       public int StatusId { get; set; }
    }

    public class ServicoAPI
    {
        public int ServicoId { get; set; }
        public string Nomeservico { get; set; }
        public decimal Preco { get; set; }
        public int TempoGasto { get; set; }
        public int StatusId { get; set; }
    }

}