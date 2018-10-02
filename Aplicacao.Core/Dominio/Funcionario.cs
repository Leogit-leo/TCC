using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aplicacao.Core.Dominio
{
    [Table("TccSalao_Funcionario")]
    public class Funcionario : IUsuario
    {
        [Key]
        public int FuncionarioId { get; set; }

        [DisplayName("Status")]
        public int StatusId { get; set; }

        public string Nome { get; set; }

        [DisplayName("Endereço")]
        public string Endereco { get; set; }

        public string Telefone { get; set; }

        [DisplayName("Salário")]
        public decimal Salario { get; set; }

        public string Login { get; set; }

        public string Senha { get; set; }

        [DisplayName("Confirmar Senha")]
        public string ConfirmarSenha { get; set; }

        public string Email { get; set; }

        public string Tipo { get; set; }

        public string Foto { get; set; }

        public virtual Status Status { get; set; }

        public virtual ICollection<RelFuncionarioServico> Servicos { get; set; }

        public virtual ICollection<Agenda> ClienteServico { get; set; }

        public virtual ICollection<RelFuncionarioNotificacao> NotificacoesDoFuncionario { get; set; }


    }

}