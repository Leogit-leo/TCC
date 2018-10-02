using Aplicacao.Core.Dominio;
using Aplicacao.Core.RepositorioEF.Base;
using Aplicacao.Core.RepositorioEF.Contexto;

namespace Aplicacao.Core.RepositorioEF
{
    public class ProdutoFiadoRepositorioEF : Repositorio<ProdutoFiado>
    {
        public ProdutoFiadoRepositorioEF(BancoContexto repo)
            : base(repo)
        {
        }
    }
}