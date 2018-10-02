namespace Aplicacao.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nome : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.TccSalao_ControleAgenda", newName: "TccSalao_RelDataHoraFuncionario");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.TccSalao_RelDataHoraFuncionario", newName: "TccSalao_ControleAgenda");
        }
    }
}
