namespace Aplicacao.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class login : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TccSalao_Cliente", "Login", c => c.String());
            AddColumn("dbo.TccSalao_Cliente", "Senha", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TccSalao_Cliente", "Senha");
            DropColumn("dbo.TccSalao_Cliente", "Login");
        }
    }
}
