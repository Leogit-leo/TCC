namespace Aplicacao.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class apagar : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TccSalao_RelDataHoraFuncionario", "DataDisponivelId", "dbo.TccSalao_DataDisponivel");
            DropForeignKey("dbo.TccSalao_RelDataHoraFuncionario", "FuncionarioId", "dbo.TccSalao_Funcionario");
            DropForeignKey("dbo.TccSalao_RelDataHoraFuncionario", "HoraDisponivelId", "dbo.TccSalao_HoraDisponivel");
            DropIndex("dbo.TccSalao_RelDataHoraFuncionario", new[] { "DataDisponivelId" });
            DropIndex("dbo.TccSalao_RelDataHoraFuncionario", new[] { "HoraDisponivelId" });
            DropIndex("dbo.TccSalao_RelDataHoraFuncionario", new[] { "FuncionarioId" });
            DropTable("dbo.TccSalao_RelDataHoraFuncionario");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TccSalao_RelDataHoraFuncionario",
                c => new
                    {
                        RelDataHoraFuncionarioId = c.Int(nullable: false, identity: true),
                        DataDisponivelId = c.Int(nullable: false),
                        HoraDisponivelId = c.Int(nullable: false),
                        FuncionarioId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RelDataHoraFuncionarioId);
            
            CreateIndex("dbo.TccSalao_RelDataHoraFuncionario", "FuncionarioId");
            CreateIndex("dbo.TccSalao_RelDataHoraFuncionario", "HoraDisponivelId");
            CreateIndex("dbo.TccSalao_RelDataHoraFuncionario", "DataDisponivelId");
            AddForeignKey("dbo.TccSalao_RelDataHoraFuncionario", "HoraDisponivelId", "dbo.TccSalao_HoraDisponivel", "HoraDisponivelId", cascadeDelete: true);
            AddForeignKey("dbo.TccSalao_RelDataHoraFuncionario", "FuncionarioId", "dbo.TccSalao_Funcionario", "FuncionarioId", cascadeDelete: true);
            AddForeignKey("dbo.TccSalao_RelDataHoraFuncionario", "DataDisponivelId", "dbo.TccSalao_DataDisponivel", "DataDisponivelId", cascadeDelete: true);
        }
    }
}
