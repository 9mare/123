namespace ConferenceManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _101 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Fee", "Delete", c => c.Boolean(nullable: false));
            AddColumn("dbo.Topic", "Delete", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Topic", "Delete");
            DropColumn("dbo.Fee", "Delete");
        }
    }
}
