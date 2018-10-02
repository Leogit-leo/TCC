using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aplicacao.Core.Dominio
{
    [Table("TccSalao_RelFuncionarioServico")]
    public class RelFuncionarioServico
    {

        public int RelFuncionarioServicoId { get; set; }

        public int FuncionarioId { get; set; }
        public int ServicoId { get; set; }

        public virtual Servico Servico { get; set; }
        public virtual Funcionario Funcionario { get; set; }

    }

}