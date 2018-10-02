namespace Aplicacao.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class status : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TccSalao_Status",
                c => new
                    {
                        StatusId = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Bloquear = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.StatusId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TccSalao_Status");
        }
    }
}
