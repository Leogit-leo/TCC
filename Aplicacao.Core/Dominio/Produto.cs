﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aplicacao.Core.Dominio
{
    [Table("TccSalao_Produto")]
    public class Produto 
    {
        [Key]
        public int ProdutoId { get; set; }

        public int StatusId { get; set; }

        public string Nome { get; set; }

        [DisplayName("Preço")]
        public decimal Preco { get; set; }

        public int Quantidade { get; set; }

        public virtual Status Status { get; set; }
    }

}