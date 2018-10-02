using Aplicacao.Core.Dominio;
using Aplicacao.Core.RepositorioEF.Base;
using Aplicacao.Core.RepositorioEF.Contexto;

namespace Aplicacao.Core.RepositorioEF
{
    public class ServicoFiadoRepositorioEF : Repositorio<ServicoFiado>
    {
        public ServicoFiadoRepositorioEF(BancoContexto repo)
            : base(repo)
        {
        }
    }
}