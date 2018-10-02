namespace Aplicacao.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class relvhjvhjnj : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TccSalao_RelDataHora",
                c => new
                    {
                        RelDataHoraId = c.Int(nullable: false, identity: true),
                        DataDisponivelId = c.Int(nullable: false),
                        HoraDisponivelId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RelDataHoraId)
                .ForeignKey("dbo.TccSalao_DataDisponivel", t => t.DataDisponivelId, cascadeDelete: true)
                .ForeignKey("dbo.TccSalao_HoraDisponivel", t => t.HoraDisponivelId, cascadeDelete: true)
                .Index(t => t.DataDisponivelId)
                .Index(t => t.HoraDisponivelId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TccSalao_RelDataHora", "HoraDisponivelId", "dbo.TccSalao_HoraDisponivel");
            DropForeignKey("dbo.TccSalao_RelDataHora", "DataDisponivelId", "dbo.TccSalao_DataDisponivel");
            DropIndex("dbo.TccSalao_RelDataHora", new[] { "HoraDisponivelId" });
            DropIndex("dbo.TccSalao_RelDataHora", new[] { "DataDisponivelId" });
            DropTable("dbo.TccSalao_RelDataHora");
        }
    }
}
