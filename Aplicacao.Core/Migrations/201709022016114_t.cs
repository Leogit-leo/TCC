namespace Aplicacao.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class t : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TccSalao_RegistroCompraFiada", "FuncionarioId", "dbo.TccSalao_Funcionario");
            DropIndex("dbo.TccSalao_RegistroCompraFiada", new[] { "FuncionarioId" });
            AlterColumn("dbo.TccSalao_RegistroCompraFiada", "FuncionarioId", c => c.Int(nullable: false));
            CreateIndex("dbo.TccSalao_RegistroCompraFiada", "FuncionarioId");
            AddForeignKey("dbo.TccSalao_RegistroCompraFiada", "FuncionarioId", "dbo.TccSalao_Funcionario", "FuncionarioId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TccSalao_RegistroCompraFiada", "FuncionarioId", "dbo.TccSalao_Funcionario");
            DropIndex("dbo.TccSalao_RegistroCompraFiada", new[] { "FuncionarioId" });
            AlterColumn("dbo.TccSalao_RegistroCompraFiada", "FuncionarioId", c => c.Int());
            CreateIndex("dbo.TccSalao_RegistroCompraFiada", "FuncionarioId");
            AddForeignKey("dbo.TccSalao_RegistroCompraFiada", "FuncionarioId", "dbo.TccSalao_Funcionario", "FuncionarioId");
        }
    }
}
