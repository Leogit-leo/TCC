namespace Aplicacao.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class funcid : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.TccSalao_Notificacao", name: "FuncionarioRemetente", newName: "FuncionarioId");
            RenameIndex(table: "dbo.TccSalao_Notificacao", name: "IX_FuncionarioRemetente", newName: "IX_FuncionarioId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.TccSalao_Notificacao", name: "IX_FuncionarioId", newName: "IX_FuncionarioRemetente");
            RenameColumn(table: "dbo.TccSalao_Notificacao", name: "FuncionarioId", newName: "FuncionarioRemetente");
        }
    }
}
