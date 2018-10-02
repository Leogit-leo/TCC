using Aplicacao.Core.Dominio;
using Aplicacao.Core.RepositorioEF.Base;
using Aplicacao.Core.RepositorioEF.Contexto;

namespace Aplicacao.Core.RepositorioEF
{
    public class ClienteRepositorioEF : Repositorio<Cliente>
    {
        public ClienteRepositorioEF(BancoContexto repo)
            : base(repo)
        {
        }
    }
}