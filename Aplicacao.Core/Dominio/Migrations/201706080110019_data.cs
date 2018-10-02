namespace Aplicacao.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class data : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TccSalao_ControleAgenda",
                c => new
                    {
                        RelDataHoraFuncionarioId = c.Int(nullable: false, identity: true),
                        FuncionarioId = c.Int(nullable: false),
                        DataDisponivelId = c.Int(nullable: false),
                        HoraDisponivelId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RelDataHoraFuncionarioId)
                .ForeignKey("dbo.TccSalao_DataDisponivel", t => t.DataDisponivelId, cascadeDelete: true)
                .ForeignKey("dbo.TccSalao_Funcionario", t => t.FuncionarioId, cascadeDelete: true)
                .ForeignKey("dbo.TccSalao_HoraDisponivel", t => t.HoraDisponivelId, cascadeDelete: true)
                .Index(t => t.FuncionarioId)
                .Index(t => t.DataDisponivelId)
                .Index(t => t.HoraDisponivelId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TccSalao_ControleAgenda", "HoraDisponivelId", "dbo.TccSalao_HoraDisponivel");
            DropForeignKey("dbo.TccSalao_ControleAgenda", "FuncionarioId", "dbo.TccSalao_Funcionario");
            DropForeignKey("dbo.TccSalao_ControleAgenda", "DataDisponivelId", "dbo.TccSalao_DataDisponivel");
            DropIndex("dbo.TccSalao_ControleAgenda", new[] { "HoraDisponivelId" });
            DropIndex("dbo.TccSalao_ControleAgenda", new[] { "DataDisponivelId" });
            DropIndex("dbo.TccSalao_ControleAgenda", new[] { "FuncionarioId" });
            DropTable("dbo.TccSalao_ControleAgenda");
        }
    }
}
