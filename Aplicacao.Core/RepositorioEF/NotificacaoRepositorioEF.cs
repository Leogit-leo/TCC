using Aplicacao.Core.Dominio;
using Aplicacao.Core.RepositorioEF.Base;
using Aplicacao.Core.RepositorioEF.Contexto;

namespace Aplicacao.Core.RepositorioEF
{
    public class NotificacaoRepositorioEF : Repositorio<Notificacao>
    {
        public NotificacaoRepositorioEF(BancoContexto repo)
            : base(repo)
        {
        }
    }
}