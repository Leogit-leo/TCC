namespace Aplicacao.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class data1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TccSalao_Foto", "DataFoto", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TccSalao_Foto", "Imagem", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TccSalao_Foto", "Imagem", c => c.String());
            DropColumn("dbo.TccSalao_Foto", "DataFoto");
        }
    }
}
