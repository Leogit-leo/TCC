namespace Aplicacao.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fotofun : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TccSalao_Foto", "FuncionarioId", c => c.Int(nullable: false));
            CreateIndex("dbo.TccSalao_Foto", "FuncionarioId");
            AddForeignKey("dbo.TccSalao_Foto", "FuncionarioId", "dbo.TccSalao_Funcionario", "FuncionarioId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TccSalao_Foto", "FuncionarioId", "dbo.TccSalao_Funcionario");
            DropIndex("dbo.TccSalao_Foto", new[] { "FuncionarioId" });
            DropColumn("dbo.TccSalao_Foto", "FuncionarioId");
        }
    }
}
