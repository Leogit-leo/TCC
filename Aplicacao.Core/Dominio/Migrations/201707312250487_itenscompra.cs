namespace Aplicacao.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class itenscompra : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TccSalao_ItemCompra", "RegistroCompra_RegistroCompraId", "dbo.TccSalao_RegistroCompra");
            DropIndex("dbo.TccSalao_ItemCompra", new[] { "RegistroCompra_RegistroCompraId" });
            RenameColumn(table: "dbo.TccSalao_ItemCompra", name: "RegistroCompra_RegistroCompraId", newName: "RegistroCompraId");
            AlterColumn("dbo.TccSalao_ItemCompra", "RegistroCompraId", c => c.Int(nullable: false));
            CreateIndex("dbo.TccSalao_ItemCompra", "RegistroCompraId");
            AddForeignKey("dbo.TccSalao_ItemCompra", "RegistroCompraId", "dbo.TccSalao_RegistroCompra", "RegistroCompraId", cascadeDelete: true);
            DropColumn("dbo.TccSalao_RegistroCompra", "ItemCompraId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TccSalao_RegistroCompra", "ItemCompraId", c => c.Int(nullable: false));
            DropForeignKey("dbo.TccSalao_ItemCompra", "RegistroCompraId", "dbo.TccSalao_RegistroCompra");
            DropIndex("dbo.TccSalao_ItemCompra", new[] { "RegistroCompraId" });
            AlterColumn("dbo.TccSalao_ItemCompra", "RegistroCompraId", c => c.Int());
            RenameColumn(table: "dbo.TccSalao_ItemCompra", name: "RegistroCompraId", newName: "RegistroCompra_RegistroCompraId");
            CreateIndex("dbo.TccSalao_ItemCompra", "RegistroCompra_RegistroCompraId");
            AddForeignKey("dbo.TccSalao_ItemCompra", "RegistroCompra_RegistroCompraId", "dbo.TccSalao_RegistroCompra", "RegistroCompraId");
        }
    }
}
