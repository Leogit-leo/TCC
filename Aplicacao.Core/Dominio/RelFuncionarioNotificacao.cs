using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aplicacao.Core.Dominio
{
    [Table("TccSalao_RelFuncionarioNotificacao")]
    public class RelFuncionarioNotificacao 
    {
        [Key]
        public int RelFuncionarioNotificacaoId { get; set; }

        public int FuncionarioId { get; set; }
        public int NotificacaoId { get; set; }

        public DateTime DataVisualizacao { get; set; }

        public virtual Funcionario Funcionario { get; set; }
        public virtual Notificacao Notificacao { get; set; }

    }

}