using Aplicacao.Core.Dominio;
using Aplicacao.Core.RepositorioEF.Base;
using Aplicacao.Core.RepositorioEF.Contexto;

namespace Aplicacao.Core.RepositorioEF
{
    public class ProdutoRepositorioEF : Repositorio<Produto>
    {
        public ProdutoRepositorioEF(BancoContexto repo)
            : base(repo)
        {
        }
    }
}