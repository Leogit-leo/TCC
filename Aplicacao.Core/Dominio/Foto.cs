using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aplicacao.Core.Dominio
{
    [Table("TccSalao_Foto")]
    public class Foto 
    {
        [Key]
        public int FotoId { get; set; }

        public int StatusId { get; set; }

        public int CategoriaFotoId { get; set; }

        public int FuncionarioId { get; set; }

        public string NomeFoto { get; set; }

        [Display(Name = "Imagem/Foto")]
        [Required(ErrorMessage ="A foto é obrigatoria")]
        public string Imagem { get; set; }
       
        public string Descricao { get; set; }

        public DateTime DataFoto { get; set; }

        public virtual Status Status { get; set; }
        public virtual CategoriaFoto CategoriaFoto { get; set; }
        public virtual Funcionario Funcionario { get; set; }
    }

}