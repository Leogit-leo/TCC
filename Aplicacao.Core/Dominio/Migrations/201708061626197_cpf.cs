namespace Aplicacao.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cpf : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TccSalao_Cliente", "CPF", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TccSalao_Cliente", "CPF");
        }
    }
}
