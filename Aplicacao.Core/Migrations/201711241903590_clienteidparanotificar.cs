namespace Aplicacao.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class clienteidparanotificar : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TccSalao_Notificacao", "ClienteId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TccSalao_Notificacao", "ClienteId");
        }
    }
}
