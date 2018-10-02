using Aplicacao.Core.Dominio;
using Aplicacao.Core.RepositorioEF.Base;
using Aplicacao.Core.RepositorioEF.Contexto;

namespace Aplicacao.Core.RepositorioEF
{
    public class FotoRepositorioEF : Repositorio<Foto>
    {
        public FotoRepositorioEF(BancoContexto repo)
            : base(repo)
        {
        }
    }
}