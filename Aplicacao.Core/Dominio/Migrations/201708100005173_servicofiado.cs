namespace Aplicacao.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class servicofiado : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TccSalao_RegistroCompraFiada", "ServicoId", c => c.Int());
            AddColumn("dbo.TccSalao_RegistroCompraFiada", "ProdutoId", c => c.Int());
            CreateIndex("dbo.TccSalao_RegistroCompraFiada", "ServicoId");
            CreateIndex("dbo.TccSalao_RegistroCompraFiada", "ProdutoId");
            AddForeignKey("dbo.TccSalao_RegistroCompraFiada", "ProdutoId", "dbo.TccSalao_Produto", "ProdutoId");
            AddForeignKey("dbo.TccSalao_RegistroCompraFiada", "ServicoId", "dbo.TccSalao_Servico", "ServicoId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TccSalao_RegistroCompraFiada", "ServicoId", "dbo.TccSalao_Servico");
            DropForeignKey("dbo.TccSalao_RegistroCompraFiada", "ProdutoId", "dbo.TccSalao_Produto");
            DropIndex("dbo.TccSalao_RegistroCompraFiada", new[] { "ProdutoId" });
            DropIndex("dbo.TccSalao_RegistroCompraFiada", new[] { "ServicoId" });
            DropColumn("dbo.TccSalao_RegistroCompraFiada", "ProdutoId");
            DropColumn("dbo.TccSalao_RegistroCompraFiada", "ServicoId");
        }
    }
}
