using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aplicacao.Core.Dominio
{
    
    public interface IUsuario
    {

       string Login { get; set; }

       string Senha { get; set; }

       string ConfirmarSenha { get; set; }

         string Email { get; set; }

        string Tipo { get; set; }
    }

}