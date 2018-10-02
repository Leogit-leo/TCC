namespace Aplicacao.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class quantidade : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TccSalao_ProdutoFiado", "Quantidade", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TccSalao_ProdutoFiado", "Quantidade");
        }
    }
}
