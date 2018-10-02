using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aplicacao.Core.Dominio
{
    [Table("TccSalao_Cliente")]
    public class Cliente 
    {
        [Key]
        public int ClienteId { get; set; }

        [DisplayName("Status")]
        public int StatusId { get; set; }

        public string Nome { get; set; }

        [DisplayName("Endereço")]
        public string Endereco { get; set; }
       
        public string Telefone { get; set; }

        public string Email { get; set; }

        public string CPF { get; set; }

        public string Login { get; set; }

        public string Senha { get; set; }

        public virtual Status Status { get; set; }

        public virtual ICollection<Agenda> FuncionarioServico { get; set; }


    }

}