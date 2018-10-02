namespace Aplicacao.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dataehorastring : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TccSalao_DataDisponivel", "Data", c => c.String());
            AlterColumn("dbo.TccSalao_HoraDisponivel", "Hora", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TccSalao_HoraDisponivel", "Hora", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TccSalao_DataDisponivel", "Data", c => c.DateTime(nullable: false));
        }
    }
}
