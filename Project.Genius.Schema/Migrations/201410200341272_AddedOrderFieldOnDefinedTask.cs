namespace Project.Genius.Schema.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedOrderFieldOnDefinedTask : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DefinedTasks", "Order", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DefinedTasks", "Order");
        }
    }
}
