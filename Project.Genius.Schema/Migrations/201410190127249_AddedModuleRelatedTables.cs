namespace Project.Genius.Schema.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedModuleRelatedTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DefinedTasks",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Duration = c.Double(nullable: false),
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
                        CreatedOn = c.DateTime(nullable: false),
                        Description = c.String(),
                        Name = c.String(),
                        Optional = c.Boolean(nullable: false),
                        UpdateOn = c.DateTime(nullable: false),
                        CreateBy_Id = c.String(maxLength: 128),
                        Type_Id = c.Int(),
                        UpdateBy_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUsers", t => t.CreateBy_Id)
                .ForeignKey("dbo.ModuleTypes", t => t.Type_Id)
                .ForeignKey("dbo.ApplicationUsers", t => t.UpdateBy_Id)
                .Index(t => t.CreateBy_Id)
                .Index(t => t.Type_Id)
                .Index(t => t.UpdateBy_Id);
            
            CreateTable(
                "dbo.ModuleTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DefinedTasks", "Owner_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.Modules", "UpdateBy_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.Modules", "Type_Id", "dbo.ModuleTypes");
            DropForeignKey("dbo.DefinedTasks", "Module_Id", "dbo.Modules");
            DropForeignKey("dbo.Modules", "CreateBy_Id", "dbo.ApplicationUsers");
            DropIndex("dbo.Modules", new[] { "UpdateBy_Id" });
            DropIndex("dbo.Modules", new[] { "Type_Id" });
            DropIndex("dbo.Modules", new[] { "CreateBy_Id" });
            DropIndex("dbo.DefinedTasks", new[] { "Owner_Id" });
            DropIndex("dbo.DefinedTasks", new[] { "Module_Id" });
            DropTable("dbo.ProductVersions");
            DropTable("dbo.ModuleTypes");
            DropTable("dbo.Modules");
            DropTable("dbo.DefinedTasks");
        }
    }
}
