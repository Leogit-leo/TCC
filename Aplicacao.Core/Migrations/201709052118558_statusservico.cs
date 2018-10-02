namespace Aplicacao.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class statusservico : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TccSalao_Servico", "StatusId", c => c.Int(nullable: false));
            CreateIndex("dbo.TccSalao_Servico", "StatusId");
            AddForeignKey("dbo.TccSalao_Servico", "StatusId", "dbo.TccSalao_Status", "StatusId", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TccSalao_Servico", "StatusId", "dbo.TccSalao_Status");
            DropIndex("dbo.TccSalao_Servico", new[] { "StatusId" });
            DropColumn("dbo.TccSalao_Servico", "StatusId");
        }
    }
}
