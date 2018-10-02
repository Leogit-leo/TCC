namespace Aplicacao.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class comissao : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TccSalao_Comissao",
                c => new
                    {
                        ComissaoId = c.Int(nullable: false, identity: true),
                        Data = c.DateTime(nullable: false),
                        FuncionarioId = c.Int(nullable: false),
                        ServicoId = c.Int(nullable: false),
                        valorComissao = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ComissaoId)
                .ForeignKey("dbo.TccSalao_Funcionario", t => t.FuncionarioId, cascadeDelete: true)
                .ForeignKey("dbo.TccSalao_Servico", t => t.ServicoId, cascadeDelete: true)
                .Index(t => t.FuncionarioId)
                .Index(t => t.ServicoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TccSalao_Comissao", "ServicoId", "dbo.TccSalao_Servico");
            DropForeignKey("dbo.TccSalao_Comissao", "FuncionarioId", "dbo.TccSalao_Funcionario");
            DropIndex("dbo.TccSalao_Comissao", new[] { "ServicoId" });
            DropIndex("dbo.TccSalao_Comissao", new[] { "FuncionarioId" });
            DropTable("dbo.TccSalao_Comissao");
        }
    }
}
