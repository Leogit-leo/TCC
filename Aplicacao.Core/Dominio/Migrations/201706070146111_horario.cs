namespace Aplicacao.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class horario : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TccSalao_HoraDisponivel", "Horario", c => c.String());
            DropColumn("dbo.TccSalao_HoraDisponivel", "Hora");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TccSalao_HoraDisponivel", "Hora", c => c.String());
            DropColumn("dbo.TccSalao_HoraDisponivel", "Horario");
        }
    }
}
