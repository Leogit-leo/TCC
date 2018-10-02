using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aplicacao.Core.Dominio
{
    [Table("TccSalao_Notificacao")]
    public class Notificacao 
    {
        [Key]
        public int NotificacaoId { get; set; }

        public string Titulo { get; set; }

        public int FuncionarioId { get; set; }

        public int? FuncionarioDestino { get; set; }

        public string Mensagem { get; set; }

        public DateTime DataNotificacao { get; set; }

        // para notificar um cliente
        public int? ClienteId { get; set; }

        public virtual Funcionario Funcionario { get; set; }

        public virtual ICollection<RelFuncionarioNotificacao> FuncionarioDaNotificacao { get; set; }
    }

}