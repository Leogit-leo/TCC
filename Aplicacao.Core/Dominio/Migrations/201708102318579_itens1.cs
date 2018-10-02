namespace Aplicacao.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class itens1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TccSalao_ItemCompraFiado", "ProdutoId", "dbo.TccSalao_Produto");
            DropForeignKey("dbo.TccSalao_ItemCompraFiado", "RegistroCompraFiadaId", "dbo.TccSalao_RegistroCompraFiada");
            DropForeignKey("dbo.TccSalao_ItemCompraFiado", "ServicoId", "dbo.TccSalao_Servico");
            DropIndex("dbo.TccSalao_ItemCompraFiado", new[] { "ProdutoId" });
            DropIndex("dbo.TccSalao_ItemCompraFiado", new[] { "ServicoId" });
            DropIndex("dbo.TccSalao_ItemCompraFiado", new[] { "RegistroCompraFiadaId" });
            CreateTable(
                "dbo.TccSalao_ItensCompraFiada",
                c => new
                    {
                        ItensCompraFiadaId = c.Int(nullable: false, identity: true),
                        RegistroCompraFiadaId = c.Int(nullable: false),
                        ServicoId = c.Int(nullable: false),
                        ProdutoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ItensCompraFiadaId)
                .ForeignKey("dbo.TccSalao_Produto", t => t.ProdutoId, cascadeDelete: true)
                .ForeignKey("dbo.TccSalao_RegistroCompraFiada", t => t.RegistroCompraFiadaId, cascadeDelete: true)
                .ForeignKey("dbo.TccSalao_Servico", t => t.ServicoId, cascadeDelete: true)
                .Index(t => t.RegistroCompraFiadaId)
                .Index(t => t.ServicoId)
                .Index(t => t.ProdutoId);
            
            DropTable("dbo.TccSalao_ItemCompraFiado");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TccSalao_ItemCompraFiado",
                c => new
                    {
                        ItemCompraFiadoId = c.Int(nullable: false, identity: true),
                        ProdutoId = c.Int(nullable: false),
                        Quantidade = c.Int(nullable: false),
                        ServicoId = c.Int(nullable: false),
                        RegistroCompraFiadaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ItemCompraFiadoId);
            
            DropForeignKey("dbo.TccSalao_ItensCompraFiada", "ServicoId", "dbo.TccSalao_Servico");
            DropForeignKey("dbo.TccSalao_ItensCompraFiada", "RegistroCompraFiadaId", "dbo.TccSalao_RegistroCompraFiada");
            DropForeignKey("dbo.TccSalao_ItensCompraFiada", "ProdutoId", "dbo.TccSalao_Produto");
            DropIndex("dbo.TccSalao_ItensCompraFiada", new[] { "ProdutoId" });
            DropIndex("dbo.TccSalao_ItensCompraFiada", new[] { "ServicoId" });
            DropIndex("dbo.TccSalao_ItensCompraFiada", new[] { "RegistroCompraFiadaId" });
            DropTable("dbo.TccSalao_ItensCompraFiada");
            CreateIndex("dbo.TccSalao_ItemCompraFiado", "RegistroCompraFiadaId");
            CreateIndex("dbo.TccSalao_ItemCompraFiado", "ServicoId");
            CreateIndex("dbo.TccSalao_ItemCompraFiado", "ProdutoId");
            AddForeignKey("dbo.TccSalao_ItemCompraFiado", "ServicoId", "dbo.TccSalao_Servico", "ServicoId", cascadeDelete: true);
            AddForeignKey("dbo.TccSalao_ItemCompraFiado", "RegistroCompraFiadaId", "dbo.TccSalao_RegistroCompraFiada", "RegistroCompraFiadaId", cascadeDelete: true);
            AddForeignKey("dbo.TccSalao_ItemCompraFiado", "ProdutoId", "dbo.TccSalao_Produto", "ProdutoId", cascadeDelete: true);
        }
    }
}
