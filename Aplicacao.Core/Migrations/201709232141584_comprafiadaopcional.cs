namespace Aplicacao.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class comprafiadaopcional : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TccSalao_Comissao", "RegistroCompraFiadaId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TccSalao_Comissao", "RegistroCompraFiadaId");
        }
    }
}
