namespace Aplicacao.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class controleagenda : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TccSalao_RelFuncionarioDataHora", "DataDisponivelId", "dbo.TccSalao_DataDisponivel");
            DropForeignKey("dbo.TccSalao_RelFuncionarioDataHora", "FuncionarioId", "dbo.TccSalao_Funcionario");
            DropForeignKey("dbo.TccSalao_RelFuncionarioDataHora", "HoraDisponivelId", "dbo.TccSalao_HoraDisponivel");
            DropIndex("dbo.TccSalao_RelFuncionarioDataHora", new[] { "FuncionarioId" });
            DropIndex("dbo.TccSalao_RelFuncionarioDataHora", new[] { "DataDisponivelId" });
            DropIndex("dbo.TccSalao_RelFuncionarioDataHora", new[] { "HoraDisponivelId" });
            CreateTable(
                "dbo.TccSalao_ControleAgenda",
                c => new
                    {
                        ControleAgendaId = c.Int(nullable: false, identity: true),
                        FuncionarioId = c.Int(nullable: false),
                        DataDisponivelId = c.Int(nullable: false),
                        HoraDisponivelId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ControleAgendaId)
                .ForeignKey("dbo.TccSalao_DataDisponivel", t => t.DataDisponivelId, cascadeDelete: true)
                .ForeignKey("dbo.TccSalao_Funcionario", t => t.FuncionarioId, cascadeDelete: true)
                .ForeignKey("dbo.TccSalao_HoraDisponivel", t => t.HoraDisponivelId, cascadeDelete: true)
                .Index(t => t.FuncionarioId)
                .Index(t => t.DataDisponivelId)
                .Index(t => t.HoraDisponivelId);
            
            DropTable("dbo.TccSalao_RelFuncionarioDataHora");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TccSalao_RelFuncionarioDataHora",
                c => new
                    {
                        RelFuncionarioDataHoraId = c.Int(nullable: false, identity: true),
                        FuncionarioId = c.Int(nullable: false),
                        DataDisponivelId = c.Int(nullable: false),
                        HoraDisponivelId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RelFuncionarioDataHoraId);
            
            DropForeignKey("dbo.TccSalao_ControleAgenda", "HoraDisponivelId", "dbo.TccSalao_HoraDisponivel");
            DropForeignKey("dbo.TccSalao_ControleAgenda", "FuncionarioId", "dbo.TccSalao_Funcionario");
            DropForeignKey("dbo.TccSalao_ControleAgenda", "DataDisponivelId", "dbo.TccSalao_DataDisponivel");
            DropIndex("dbo.TccSalao_ControleAgenda", new[] { "HoraDisponivelId" });
            DropIndex("dbo.TccSalao_ControleAgenda", new[] { "DataDisponivelId" });
            DropIndex("dbo.TccSalao_ControleAgenda", new[] { "FuncionarioId" });
            DropTable("dbo.TccSalao_ControleAgenda");
            CreateIndex("dbo.TccSalao_RelFuncionarioDataHora", "HoraDisponivelId");
            CreateIndex("dbo.TccSalao_RelFuncionarioDataHora", "DataDisponivelId");
            CreateIndex("dbo.TccSalao_RelFuncionarioDataHora", "FuncionarioId");
            AddForeignKey("dbo.TccSalao_RelFuncionarioDataHora", "HoraDisponivelId", "dbo.TccSalao_HoraDisponivel", "HoraDisponivelId", cascadeDelete: true);
            AddForeignKey("dbo.TccSalao_RelFuncionarioDataHora", "FuncionarioId", "dbo.TccSalao_Funcionario", "FuncionarioId", cascadeDelete: true);
            AddForeignKey("dbo.TccSalao_RelFuncionarioDataHora", "DataDisponivelId", "dbo.TccSalao_DataDisponivel", "DataDisponivelId", cascadeDelete: true);
        }
    }
}
