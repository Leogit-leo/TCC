namespace Aplicacao.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class servfiado : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TccSalao_ItensCompraFiada", "ProdutoId", "dbo.TccSalao_Produto");
            DropForeignKey("dbo.TccSalao_ItensCompraFiada", "RegistroCompraFiadaId", "dbo.TccSalao_RegistroCompraFiada");
            DropForeignKey("dbo.TccSalao_ItensCompraFiada", "ServicoId", "dbo.TccSalao_Servico");
            DropIndex("dbo.TccSalao_ItensCompraFiada", new[] { "RegistroCompraFiadaId" });
            DropIndex("dbo.TccSalao_ItensCompraFiada", new[] { "ServicoId" });
            DropIndex("dbo.TccSalao_ItensCompraFiada", new[] { "ProdutoId" });
            CreateTable(
                "dbo.TccSalao_ServicoFiado",
                c => new
                    {
                        ServicoFiadoId = c.Int(nullable: false, identity: true),
                        RegistroCompraFiadaId = c.Int(nullable: false),
                        ServicoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ServicoFiadoId)
                .ForeignKey("dbo.TccSalao_RegistroCompraFiada", t => t.RegistroCompraFiadaId, cascadeDelete: true)
                .ForeignKey("dbo.TccSalao_Servico", t => t.ServicoId, cascadeDelete: true)
                .Index(t => t.RegistroCompraFiadaId)
                .Index(t => t.ServicoId);
            
            DropTable("dbo.TccSalao_ItensCompraFiada");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TccSalao_ItensCompraFiada",
                c => new
                    {
                        ItensCompraFiadaId = c.Int(nullable: false, identity: true),
                        RegistroCompraFiadaId = c.Int(nullable: false),
                        ServicoId = c.Int(nullable: false),
                        ProdutoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ItensCompraFiadaId);
            
            DropForeignKey("dbo.TccSalao_ServicoFiado", "ServicoId", "dbo.TccSalao_Servico");
            DropForeignKey("dbo.TccSalao_ServicoFiado", "RegistroCompraFiadaId", "dbo.TccSalao_RegistroCompraFiada");
            DropIndex("dbo.TccSalao_ServicoFiado", new[] { "ServicoId" });
            DropIndex("dbo.TccSalao_ServicoFiado", new[] { "RegistroCompraFiadaId" });
            DropTable("dbo.TccSalao_ServicoFiado");
            CreateIndex("dbo.TccSalao_ItensCompraFiada", "ProdutoId");
            CreateIndex("dbo.TccSalao_ItensCompraFiada", "ServicoId");
            CreateIndex("dbo.TccSalao_ItensCompraFiada", "RegistroCompraFiadaId");
            AddForeignKey("dbo.TccSalao_ItensCompraFiada", "ServicoId", "dbo.TccSalao_Servico", "ServicoId", cascadeDelete: true);
            AddForeignKey("dbo.TccSalao_ItensCompraFiada", "RegistroCompraFiadaId", "dbo.TccSalao_RegistroCompraFiada", "RegistroCompraFiadaId", cascadeDelete: true);
            AddForeignKey("dbo.TccSalao_ItensCompraFiada", "ProdutoId", "dbo.TccSalao_Produto", "ProdutoId", cascadeDelete: true);
        }
    }
}
