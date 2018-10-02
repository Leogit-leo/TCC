using Aplicacao.Core.Dominio;
using Aplicacao.Core.RepositorioEF.Base;
using Aplicacao.Core.RepositorioEF.Contexto;

namespace Aplicacao.Core.RepositorioEF
{
    public class ServicoRepositorioEF : Repositorio<Servico>
    {
        public ServicoRepositorioEF(BancoContexto repo)
            : base(repo)
        {
        }
    }
}