namespace Aplicacao.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dataehora : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TccSalao_DataDisponivel",
                c => new
                    {
                        DataDisponivelId = c.Int(nullable: false, identity: true),
                        Data = c.DateTime(nullable: false),
                        Disponivel = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.DataDisponivelId);
            
            CreateTable(
                "dbo.TccSalao_HoraDisponivel",
                c => new
                    {
                        HoraDisponivelId = c.Int(nullable: false, identity: true),
                        Hora = c.DateTime(nullable: false),
                        Disponivel = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.HoraDisponivelId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TccSalao_HoraDisponivel");
            DropTable("dbo.TccSalao_DataDisponivel");
        }
    }
}
