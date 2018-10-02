namespace Aplicacao.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class negativa : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TccSalao_ControleAgenda", "DataDisponivelId", "dbo.TccSalao_DataDisponivel");
            DropForeignKey("dbo.TccSalao_ControleAgenda", "FuncionarioId", "dbo.TccSalao_Funcionario");
            DropForeignKey("dbo.TccSalao_ControleAgenda", "HoraDisponivelId", "dbo.TccSalao_HoraDisponivel");
            DropIndex("dbo.TccSalao_ControleAgenda", new[] { "FuncionarioId" });
            DropIndex("dbo.TccSalao_ControleAgenda", new[] { "DataDisponivelId" });
            DropIndex("dbo.TccSalao_ControleAgenda", new[] { "HoraDisponivelId" });
            DropTable("dbo.TccSalao_ControleAgenda");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TccSalao_ControleAgenda",
                c => new
                    {
                        ControleAgendaId = c.Int(nullable: false, identity: true),
                        FuncionarioId = c.Int(nullable: false),
                        DataDisponivelId = c.Int(nullable: false),
                        HoraDisponivelId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ControleAgendaId);
            
            CreateIndex("dbo.TccSalao_ControleAgenda", "HoraDisponivelId");
            CreateIndex("dbo.TccSalao_ControleAgenda", "DataDisponivelId");
            CreateIndex("dbo.TccSalao_ControleAgenda", "FuncionarioId");
            AddForeignKey("dbo.TccSalao_ControleAgenda", "HoraDisponivelId", "dbo.TccSalao_HoraDisponivel", "HoraDisponivelId", cascadeDelete: true);
            AddForeignKey("dbo.TccSalao_ControleAgenda", "FuncionarioId", "dbo.TccSalao_Funcionario", "FuncionarioId", cascadeDelete: true);
            AddForeignKey("dbo.TccSalao_ControleAgenda", "DataDisponivelId", "dbo.TccSalao_DataDisponivel", "DataDisponivelId", cascadeDelete: true);
        }
    }
}
