namespace Aplicacao.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modelodata : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TccSalao_DataDisponivel", "Data", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TccSalao_DataDisponivel", "Data", c => c.String());
        }
    }
}
