namespace Project.Genius.Schema.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddModuleAndDefinedTaskTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DefinedTasks",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Duration = c.Single(nullable: false),
                        Instruction = c.String(),
                        Name = c.String(),
                        Module_Id = c.Guid(),
                        Owner_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Modules", t => t.Module_Id)
                .ForeignKey("dbo.ApplicationUsers", t => t.Owner_Id)
                .Index(t => t.Module_Id)
                .Index(t => t.Owner_Id);
            
            CreateTable(
                "dbo.Modules",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Caption = c.String(),
                        Description = c.String(),
                        Name = c.String(),
                        Owner_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUsers", t => t.Owner_Id)
                .Index(t => t.Owner_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DefinedTasks", "Owner_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.DefinedTasks", "Module_Id", "dbo.Modules");
            DropForeignKey("dbo.Modules", "Owner_Id", "dbo.ApplicationUsers");
            DropIndex("dbo.Modules", new[] { "Owner_Id" });
            DropIndex("dbo.DefinedTasks", new[] { "Owner_Id" });
            DropIndex("dbo.DefinedTasks", new[] { "Module_Id" });
            DropTable("dbo.Modules");
            DropTable("dbo.DefinedTasks");
        }
    }
}
