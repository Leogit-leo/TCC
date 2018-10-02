using Aplicacao.Core.Dominio;
using Aplicacao.Core.RepositorioEF.Base;
using Aplicacao.Core.RepositorioEF.Contexto;

namespace Aplicacao.Core.RepositorioEF
{
    public class StatusRepositorioEF : Repositorio<Status>
    {
        public StatusRepositorioEF(BancoContexto repo)
            : base(repo)
        {
        }
    }
}