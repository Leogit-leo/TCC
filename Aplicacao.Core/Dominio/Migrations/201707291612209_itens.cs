namespace Aplicacao.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class itens : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TccSalao_RegistroCompra", "ItemCompraId", "dbo.TccSalao_ItemCompra");
            DropIndex("dbo.TccSalao_RegistroCompra", new[] { "ItemCompraId" });
            AddColumn("dbo.TccSalao_ItemCompra", "RegistroCompra_RegistroCompraId", c => c.Int());
            CreateIndex("dbo.TccSalao_ItemCompra", "RegistroCompra_RegistroCompraId");
            AddForeignKey("dbo.TccSalao_ItemCompra", "RegistroCompra_RegistroCompraId", "dbo.TccSalao_RegistroCompra", "RegistroCompraId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TccSalao_ItemCompra", "RegistroCompra_RegistroCompraId", "dbo.TccSalao_RegistroCompra");
            DropIndex("dbo.TccSalao_ItemCompra", new[] { "RegistroCompra_RegistroCompraId" });
            DropColumn("dbo.TccSalao_ItemCompra", "RegistroCompra_RegistroCompraId");
            CreateIndex("dbo.TccSalao_RegistroCompra", "ItemCompraId");
            AddForeignKey("dbo.TccSalao_RegistroCompra", "ItemCompraId", "dbo.TccSalao_ItemCompra", "ItemCompraId", cascadeDelete: true);
        }
    }
}
