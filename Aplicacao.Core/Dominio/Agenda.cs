    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aplicacao.Core.Dominio
{
    [Table("TccSalao_Agenda")]
    public class Agenda
    {
        public int AgendaId { get; set; }

        [NotMapped]
        public DateTime? DataAgendamento { get; set; }

        public DateTime Inicio { get; set; }

        public DateTime Fim { get; set; }

        public bool Pago { get; set; }

        public int FuncionarioId { get; set; }
        public int ClienteId { get; set; }
        public int ServicoId { get; set; }

        [NotMapped]
        public decimal TempoGasto { get; set; }

        public virtual Servico Servico { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual Funcionario Funcionario { get; set; }

    }

}