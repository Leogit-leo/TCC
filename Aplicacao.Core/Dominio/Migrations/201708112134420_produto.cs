namespace Aplicacao.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class produto : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TccSalao_ProdutoFiado",
                c => new
                    {
                        ProdutoFiadoId = c.Int(nullable: false, identity: true),
                        RegistroCompraFiadaId = c.Int(nullable: false),
                        ProdutoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProdutoFiadoId)
                .ForeignKey("dbo.TccSalao_Produto", t => t.ProdutoId, cascadeDelete: true)
                .ForeignKey("dbo.TccSalao_RegistroCompraFiada", t => t.RegistroCompraFiadaId, cascadeDelete: true)
                .Index(t => t.RegistroCompraFiadaId)
                .Index(t => t.ProdutoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TccSalao_ProdutoFiado", "RegistroCompraFiadaId", "dbo.TccSalao_RegistroCompraFiada");
            DropForeignKey("dbo.TccSalao_ProdutoFiado", "ProdutoId", "dbo.TccSalao_Produto");
            DropIndex("dbo.TccSalao_ProdutoFiado", new[] { "ProdutoId" });
            DropIndex("dbo.TccSalao_ProdutoFiado", new[] { "RegistroCompraFiadaId" });
            DropTable("dbo.TccSalao_ProdutoFiado");
        }
    }
}
