using Aplicacao.Core.Dominio;
using Aplicacao.Core.RepositorioEF.Base;
using Aplicacao.Core.RepositorioEF.Contexto;

namespace Aplicacao.Core.RepositorioEF
{
    public class FuncionarioRepositorioEF : Repositorio<Funcionario>
    {
        public FuncionarioRepositorioEF(BancoContexto repo)
            : base(repo)
        {
        }
    }
}