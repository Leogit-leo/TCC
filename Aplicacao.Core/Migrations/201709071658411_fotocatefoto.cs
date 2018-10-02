namespace Aplicacao.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fotocatefoto : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TccSalao_CategoriaFoto",
                c => new
                    {
                        CategoriaFotoId = c.Int(nullable: false, identity: true),
                        StatusId = c.Int(nullable: false),
                        Nome = c.String(),
                    })
                .PrimaryKey(t => t.CategoriaFotoId)
                .ForeignKey("dbo.TccSalao_Status", t => t.StatusId, cascadeDelete: false)
                .Index(t => t.StatusId);
            
            CreateTable(
                "dbo.TccSalao_Foto",
                c => new
                    {
                        FotoId = c.Int(nullable: false, identity: true),
                        StatusId = c.Int(nullable: false),
                        CategoriaFotoId = c.Int(nullable: false),
                        NomeFoto = c.String(),
                        Imagem = c.String(),
                        Descricao = c.String(),
                    })
                .PrimaryKey(t => t.FotoId)
                .ForeignKey("dbo.TccSalao_CategoriaFoto", t => t.CategoriaFotoId, cascadeDelete: true)
                .ForeignKey("dbo.TccSalao_Status", t => t.StatusId, cascadeDelete: false)
                .Index(t => t.StatusId)
                .Index(t => t.CategoriaFotoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TccSalao_Foto", "StatusId", "dbo.TccSalao_Status");
            DropForeignKey("dbo.TccSalao_Foto", "CategoriaFotoId", "dbo.TccSalao_CategoriaFoto");
            DropForeignKey("dbo.TccSalao_CategoriaFoto", "StatusId", "dbo.TccSalao_Status");
            DropIndex("dbo.TccSalao_Foto", new[] { "CategoriaFotoId" });
            DropIndex("dbo.TccSalao_Foto", new[] { "StatusId" });
            DropIndex("dbo.TccSalao_CategoriaFoto", new[] { "StatusId" });
            DropTable("dbo.TccSalao_Foto");
            DropTable("dbo.TccSalao_CategoriaFoto");
        }
    }
}
