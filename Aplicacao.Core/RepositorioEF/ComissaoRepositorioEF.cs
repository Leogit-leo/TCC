using Aplicacao.Core.Dominio;
using Aplicacao.Core.RepositorioEF.Base;
using Aplicacao.Core.RepositorioEF.Contexto;

namespace Aplicacao.Core.RepositorioEF
{
    public class ComissaoRepositorioEF : Repositorio<Comissao>
    {
        public ComissaoRepositorioEF(BancoContexto repo)
            : base(repo)
        {
        }
    }
}