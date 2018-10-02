using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aplicacao.Core.Dominio
{
    [Table("TccSalao_Status")]
    public class Status 
    {
        [Key]
        public int StatusId { get; set; }

        public string Nome { get; set; }

        public bool Bloquear { get; set; }

        
    }

}