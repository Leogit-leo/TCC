using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aplicacao.Core.Dominio
{
    [Table("TccSalao_Comissao")]
    public class Comissao
    {
        [Key]
        public int ComissaoId { get; set; }

        public DateTime Data { get; set; }

        public int? AgendaId { get; set; }

        public int? RegistroCompraFiadaId { get; set; }

        public int FuncionarioId { get; set; }

        public int ServicoId { get; set; }

        public decimal valorComissao { get; set; }

        public virtual Funcionario Funcionario { get; set; }
        public virtual Servico Servico { get; set; }
        public virtual Agenda Agenda { get; set; }

        [NotMapped]
        public string ValorParaGrafico
        {
            get
            {
                //o N2 pega o primeiro numero e mais dois numeros na sequencia se fosse o N1 pegava 
                //o primeiro numero e mais um na sequencia e o . separa estes numeros que sao valores de real
                // o replace que define as separacoes
                return this.valorComissao.ToString("N2").Replace(",", ".");
            }
        }

    }

}