using Aplicacao.Core.Dominio;
using System.Data.Entity;

namespace Aplicacao.Core.RepositorioEF.Contexto
{
    public class BancoContexto : DbContext
    {

        public DbSet<Funcionario> Funcionario { get; set; }
        public DbSet<Servico> Servico { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<RelFuncionarioServico> RelFuncionarioServico { get; set; }
        public DbSet<Agenda> Agenda { get; set; }
        public DbSet<Notificacao> Notificacao { get; set; }
        public DbSet<RelFuncionarioNotificacao> RelFuncionarioNotificacao { get; set; }
        public DbSet<RegistroCompra> RegistroCompra { get; set; }
        public DbSet<ItemCompra> ItemCompra { get; set; }
        public DbSet<RegistroCompraFiada> RegistroCompraFiada { get; set; }
        public DbSet<ServicoFiado> ServicoFiado { get; set; }
        public DbSet<ProdutoFiado> ProdutoFiado { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<CategoriaFoto> CategoriaFoto { get; set; }
        public DbSet<Foto> Foto { get; set; }
        public DbSet<Comissao> Comissao { get; set; }
        public BancoContexto()
            : base("TccSalaoConfig")
        {
            Database.SetInitializer<BancoContexto>(null);
        }

    }
}
