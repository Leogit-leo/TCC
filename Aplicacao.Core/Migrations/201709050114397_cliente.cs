namespace Aplicacao.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cliente : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TccSalao_Cliente", "StatusId", c => c.Int(nullable: false));
            CreateIndex("dbo.TccSalao_Cliente", "StatusId");
            AddForeignKey("dbo.TccSalao_Cliente", "StatusId", "dbo.TccSalao_Status", "StatusId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TccSalao_Cliente", "StatusId", "dbo.TccSalao_Status");
            DropIndex("dbo.TccSalao_Cliente", new[] { "StatusId" });
            DropColumn("dbo.TccSalao_Cliente", "StatusId");
        }
    }
}
