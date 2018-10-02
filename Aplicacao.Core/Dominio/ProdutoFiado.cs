using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aplicacao.Core.Dominio
{
    [Table("TccSalao_ProdutoFiado")]
    public class ProdutoFiado 
    {
        [Key]
        public int ProdutoFiadoId { get; set; }

        public int RegistroCompraFiadaId { get; set; }

        public int ProdutoId { get; set; }
        
        public int Quantidade { get; set; }

        public virtual RegistroCompraFiada RegistroCompraFiada { get; set; }
        public virtual Produto Produto { get; set; }


        public decimal valorTotalPagar
        {
            get
            {
                return Quantidade * Produto.Preco;
            }
        }


    }

}