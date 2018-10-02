namespace Aplicacao.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datahora : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TccSalao_RelDataHoraFuncionario", "DataDisponivelId", "dbo.TccSalao_DataDisponivel");
            DropForeignKey("dbo.TccSalao_RelDataHoraFuncionario", "FuncionarioId", "dbo.TccSalao_Funcionario");
            DropForeignKey("dbo.TccSalao_RelDataHoraFuncionario", "HoraDisponivelId", "dbo.TccSalao_HoraDisponivel");
            DropIndex("dbo.TccSalao_RelDataHoraFuncionario", new[] { "FuncionarioId" });
            DropIndex("dbo.TccSalao_RelDataHoraFuncionario", new[] { "DataDisponivelId" });
            DropIndex("dbo.TccSalao_RelDataHoraFuncionario", new[] { "HoraDisponivelId" });
            CreateTable(
                "dbo.TccSalao_RelDataHora",
                c => new
                    {
                        RelDataHoraId = c.Int(nullable: false, identity: true),
                        DataDisponivelId = c.Int(nullable: false),
                        HoraDisponivelId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RelDataHoraId)
                .ForeignKey("dbo.TccSalao_DataDisponivel", t => t.DataDisponivelId, cascadeDelete: true)
                .ForeignKey("dbo.TccSalao_HoraDisponivel", t => t.HoraDisponivelId, cascadeDelete: true)
                .Index(t => t.DataDisponivelId)
                .Index(t => t.HoraDisponivelId);
            
            DropTable("dbo.TccSalao_RelDataHoraFuncionario");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TccSalao_RelDataHoraFuncionario",
                c => new
                    {
                        RelDataHoraFuncionarioId = c.Int(nullable: false, identity: true),
                        FuncionarioId = c.Int(nullable: false),
                        DataDisponivelId = c.Int(nullable: false),
                        HoraDisponivelId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RelDataHoraFuncionarioId);
            
            DropForeignKey("dbo.TccSalao_RelDataHora", "HoraDisponivelId", "dbo.TccSalao_HoraDisponivel");
            DropForeignKey("dbo.TccSalao_RelDataHora", "DataDisponivelId", "dbo.TccSalao_DataDisponivel");
            DropIndex("dbo.TccSalao_RelDataHora", new[] { "HoraDisponivelId" });
            DropIndex("dbo.TccSalao_RelDataHora", new[] { "DataDisponivelId" });
            DropTable("dbo.TccSalao_RelDataHora");
            CreateIndex("dbo.TccSalao_RelDataHoraFuncionario", "HoraDisponivelId");
            CreateIndex("dbo.TccSalao_RelDataHoraFuncionario", "DataDisponivelId");
            CreateIndex("dbo.TccSalao_RelDataHoraFuncionario", "FuncionarioId");
            AddForeignKey("dbo.TccSalao_RelDataHoraFuncionario", "HoraDisponivelId", "dbo.TccSalao_HoraDisponivel", "HoraDisponivelId", cascadeDelete: true);
            AddForeignKey("dbo.TccSalao_RelDataHoraFuncionario", "FuncionarioId", "dbo.TccSalao_Funcionario", "FuncionarioId", cascadeDelete: true);
            AddForeignKey("dbo.TccSalao_RelDataHoraFuncionario", "DataDisponivelId", "dbo.TccSalao_DataDisponivel", "DataDisponivelId", cascadeDelete: true);
        }
    }
}
