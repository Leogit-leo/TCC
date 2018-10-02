namespace Aplicacao.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class produtostatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TccSalao_Produto", "StatusId", c => c.Int(nullable: false));
            CreateIndex("dbo.TccSalao_Produto", "StatusId");
            AddForeignKey("dbo.TccSalao_Produto", "StatusId", "dbo.TccSalao_Status", "StatusId", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TccSalao_Produto", "StatusId", "dbo.TccSalao_Status");
            DropIndex("dbo.TccSalao_Produto", new[] { "StatusId" });
            DropColumn("dbo.TccSalao_Produto", "StatusId");
        }
    }
}
