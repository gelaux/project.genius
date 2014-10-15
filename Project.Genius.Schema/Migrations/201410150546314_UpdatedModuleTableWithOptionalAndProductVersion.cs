namespace Project.Genius.Schema.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedModuleTableWithOptionalAndProductVersion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductVersions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ReleaseData = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Modules", "Optional", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Modules", "Optional");
            DropTable("dbo.ProductVersions");
        }
    }
}
