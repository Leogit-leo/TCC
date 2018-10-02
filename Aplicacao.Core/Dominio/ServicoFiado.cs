using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aplicacao.Core.Dominio
{
    [Table("TccSalao_ServicoFiado")]
    public class ServicoFiado 
    {
        [Key]
        public int ServicoFiadoId { get; set; }

        public int RegistroCompraFiadaId { get; set; }

        public int ServicoId { get; set; }

        public virtual RegistroCompraFiada RegistroCompraFiada { get; set; }
        public virtual Servico Servico { get; set; }
       

    }

}