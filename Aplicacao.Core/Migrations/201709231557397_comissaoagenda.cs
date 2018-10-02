namespace Aplicacao.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class comissaoagenda : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TccSalao_Comissao", "AgendaId", c => c.Int(nullable: false));
            CreateIndex("dbo.TccSalao_Comissao", "AgendaId");
            AddForeignKey("dbo.TccSalao_Comissao", "AgendaId", "dbo.TccSalao_Agenda", "AgendaId", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TccSalao_Comissao", "AgendaId", "dbo.TccSalao_Agenda");
            DropIndex("dbo.TccSalao_Comissao", new[] { "AgendaId" });
            DropColumn("dbo.TccSalao_Comissao", "AgendaId");
        }
    }
}
