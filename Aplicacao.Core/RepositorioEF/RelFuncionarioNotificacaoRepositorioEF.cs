using Aplicacao.Core.Dominio;
using Aplicacao.Core.RepositorioEF.Base;
using Aplicacao.Core.RepositorioEF.Contexto;

namespace Aplicacao.Core.RepositorioEF
{
    public class RelFuncionarioNotificacaoRepositorioEF : Repositorio<RelFuncionarioNotificacao>
    {
        public RelFuncionarioNotificacaoRepositorioEF(BancoContexto repo)
            : base(repo)
        {
        }
    }
}