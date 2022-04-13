namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Resolucao : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Eventoes", "Descricao", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Eventoes", "Descricao");
        }
    }
}
