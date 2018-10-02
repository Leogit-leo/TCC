using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aplicacao.Core.Dominio
{
    [Table("TccSalao_RegistroCompra")]
    public class RegistroCompra
    {
        [Key]
        public int RegistroCompraId { get; set; }
        public DateTime DataCompra { get; set; }
        public int ClienteId { get; set; }


        public virtual Cliente Cliente { get; set; }

        public virtual ICollection<ItemCompra> Itens { get; set; }


    }

}