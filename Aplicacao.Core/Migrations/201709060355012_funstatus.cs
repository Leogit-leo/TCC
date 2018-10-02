namespace Aplicacao.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class funstatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TccSalao_Funcionario", "StatusId", c => c.Int(nullable: false));
            CreateIndex("dbo.TccSalao_Funcionario", "StatusId");
            AddForeignKey("dbo.TccSalao_Funcionario", "StatusId", "dbo.TccSalao_Status", "StatusId", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TccSalao_Funcionario", "StatusId", "dbo.TccSalao_Status");
            DropIndex("dbo.TccSalao_Funcionario", new[] { "StatusId" });
            DropColumn("dbo.TccSalao_Funcionario", "StatusId");
        }
    }
}
