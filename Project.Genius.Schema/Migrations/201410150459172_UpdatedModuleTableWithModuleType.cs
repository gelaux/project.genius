namespace Project.Genius.Schema.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedModuleTableWithModuleType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Modules", "Type", c => c.Int(nullable: false));
            DropColumn("dbo.Modules", "Caption");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Modules", "Caption", c => c.String());
            DropColumn("dbo.Modules", "Type");
        }
    }
}
