namespace Aplicacao.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class data : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TccSalao_RegistroCompraFiada", "DataParaPagamento", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TccSalao_RegistroCompraFiada", "DataParaPagamento", c => c.Int(nullable: false));
        }
    }
}
