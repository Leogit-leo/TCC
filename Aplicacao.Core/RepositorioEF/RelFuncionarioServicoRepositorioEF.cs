using Aplicacao.Core.Dominio;
using Aplicacao.Core.RepositorioEF.Base;
using Aplicacao.Core.RepositorioEF.Contexto;

namespace Aplicacao.Core.RepositorioEF
{
    public class RelFuncionarioServicoRepositorioEF : Repositorio<RelFuncionarioServico>
    {
        public RelFuncionarioServicoRepositorioEF(BancoContexto repo)
            : base(repo)
        {
        }
    }
}