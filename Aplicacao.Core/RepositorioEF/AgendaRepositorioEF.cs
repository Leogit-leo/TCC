using Aplicacao.Core.Dominio;
using Aplicacao.Core.RepositorioEF.Base;
using Aplicacao.Core.RepositorioEF.Contexto;

namespace Aplicacao.Core.RepositorioEF
{
    public class AgendaRepositorioEF : Repositorio<Agenda>
    {
        public AgendaRepositorioEF(BancoContexto repo)
            : base(repo)
        {
        }
    }
}