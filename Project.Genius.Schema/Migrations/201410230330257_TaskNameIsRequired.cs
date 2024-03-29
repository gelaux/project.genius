namespace Project.Genius.Schema.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TaskNameIsRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DefinedTasks", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DefinedTasks", "Name", c => c.String());
        }
    }
}
