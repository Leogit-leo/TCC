using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aplicacao.Core.Dominio
{
    [Table("TccSalao_RegistroCompraFiada")]
    public class RegistroCompraFiada 
    {
        [Key]
        public int RegistroCompraFiadaId { get; set; }
        public string Tipo { get; set; }
        public DateTime DataCompra { get; set; }
        public int ClienteId { get; set; }
        public int FuncionarioId { get; set; }
        public DateTime? DataParaPagamento { get; set; }
        public bool Pago { get; set; }

        public virtual Cliente Cliente { get; set; }
        public virtual ICollection<ServicoFiado> ServicosFiado { get; set; }
        public virtual ICollection<ProdutoFiado> ProdutosFiado { get; set; }
        public virtual Funcionario Funcionario { get; set; }


    }

}