namespace Aplicacao.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fiado : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => t.ItemCompraFiadoId)
                .ForeignKey("dbo.TccSalao_Produto", t => t.ProdutoId, cascadeDelete: true)
                .ForeignKey("dbo.TccSalao_RegistroCompraFiada", t => t.RegistroCompraFiadaId, cascadeDelete: true)
                .ForeignKey("dbo.TccSalao_Servico", t => t.ServicoId, cascadeDelete: true)
                .Index(t => t.ProdutoId)
                .Index(t => t.ServicoId)
                .Index(t => t.RegistroCompraFiadaId);
            
            CreateTable(
                "dbo.TccSalao_RegistroCompraFiada",
                c => new
                    {
                        RegistroCompraFiadaId = c.Int(nullable: false, identity: true),
                        DataCompra = c.DateTime(nullable: false),
                        ClienteId = c.Int(nullable: false),
                        DataParaPagamento = c.Int(nullable: false),
                        Pago = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.RegistroCompraFiadaId)
                .ForeignKey("dbo.TccSalao_Cliente", t => t.ClienteId, cascadeDelete: true)
                .Index(t => t.ClienteId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TccSalao_ItemCompraFiado", "ServicoId", "dbo.TccSalao_Servico");
            DropForeignKey("dbo.TccSalao_ItemCompraFiado", "RegistroCompraFiadaId", "dbo.TccSalao_RegistroCompraFiada");
            DropForeignKey("dbo.TccSalao_RegistroCompraFiada", "ClienteId", "dbo.TccSalao_Cliente");
            DropForeignKey("dbo.TccSalao_ItemCompraFiado", "ProdutoId", "dbo.TccSalao_Produto");
            DropIndex("dbo.TccSalao_RegistroCompraFiada", new[] { "ClienteId" });
            DropIndex("dbo.TccSalao_ItemCompraFiado", new[] { "RegistroCompraFiadaId" });
            DropIndex("dbo.TccSalao_ItemCompraFiado", new[] { "ServicoId" });
            DropIndex("dbo.TccSalao_ItemCompraFiado", new[] { "ProdutoId" });
            DropTable("dbo.TccSalao_RegistroCompraFiada");
            DropTable("dbo.TccSalao_ItemCompraFiado");
        }
    }
}
