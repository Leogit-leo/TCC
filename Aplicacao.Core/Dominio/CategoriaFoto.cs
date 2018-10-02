using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aplicacao.Core.Dominio
{
    [Table("TccSalao_CategoriaFoto")]
    public class CategoriaFoto 
    {
        [Key]
        public int CategoriaFotoId { get; set; }

        public int StatusId { get; set; }

        public string Nome { get; set; }

        public virtual Status Status { get; set; }
    }

}