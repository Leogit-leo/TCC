using Aplicacao.Core.Dominio;
using Aplicacao.Core.RepositorioEF.Base;
using Aplicacao.Core.RepositorioEF.Contexto;

namespace Aplicacao.Core.RepositorioEF
{
    public class CategoriaFotoRepositorioEF : Repositorio<CategoriaFoto>
    {
        public CategoriaFotoRepositorioEF(BancoContexto repo)
            : base(repo)
        {
        }
    }
}