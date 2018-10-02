using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aplicacao.Core.Dominio
{
    [Table("TccSalao_ItemCompra")]
    public class ItemCompra
    {
        [Key]
        public int ItemCompraId { get; set; }

        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }

        public int RegistroCompraId { get; set; }

        public decimal valorTotal
        {
            get
            {
                return Quantidade * Produto.Preco; 
            }
        }

        public virtual RegistroCompra RegistroCompra { get; set; }
        public virtual Produto Produto { get; set; }
    }

}