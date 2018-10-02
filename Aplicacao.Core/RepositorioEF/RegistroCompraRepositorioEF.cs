using Aplicacao.Core.Dominio;
using Aplicacao.Core.RepositorioEF.Base;
using Aplicacao.Core.RepositorioEF.Contexto;

namespace Aplicacao.Core.RepositorioEF
{
    public class RegistroCompraRepositorioEF : Repositorio<RegistroCompra>
    {
        public RegistroCompraRepositorioEF(BancoContexto repo)
            : base(repo)
        {
        }
    }
}