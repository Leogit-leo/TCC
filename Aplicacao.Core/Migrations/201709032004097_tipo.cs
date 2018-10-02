namespace Aplicacao.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tipo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TccSalao_RegistroCompraFiada", "Tipo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TccSalao_RegistroCompraFiada", "Tipo");
        }
    }
}
