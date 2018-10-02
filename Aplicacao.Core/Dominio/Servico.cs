using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aplicacao.Core.Dominio
{
    [Table("TccSalao_Servico")]
    public class Servico
    {
        [Key]
        public int ServicoId { get; set; }

        [DisplayName("Status")]
        public int StatusId { get; set; }

        [DisplayName("Nome")]
        public string Nomeservico { get; set; }

        [DisplayName("Preço")]
        public decimal Preco { get; set; }

        [DisplayName("Tempo Gasto")]
        public int TempoGasto { get; set; }

        public virtual Status Status { get; set; }

        public virtual ICollection<RelFuncionarioServico> Funcionario { get; set; }


        public virtual ICollection<Agenda> FuncionarioCliente { get; set; }

    }

}