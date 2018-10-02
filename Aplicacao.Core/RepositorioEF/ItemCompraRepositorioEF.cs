using Aplicacao.Core.Dominio;
using Aplicacao.Core.RepositorioEF.Base;
using Aplicacao.Core.RepositorioEF.Contexto;

namespace Aplicacao.Core.RepositorioEF
{
    public class ItemCompraRepositorioEF : Repositorio<ItemCompra>
    {
        public ItemCompraRepositorioEF(BancoContexto repo)
            : base(repo)
        {
        }
    }
}