namespace Project.Genius.Schema.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedModuleTableToAddCaptionField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Modules", "Caption", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Modules", "Caption");
        }
    }
}
