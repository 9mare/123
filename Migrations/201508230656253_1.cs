namespace ConferenceManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Conference", "Delete", c => c.Boolean(nullable: false));
            DropColumn("dbo.Conference", "SystemEmail");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Conference", "SystemEmail", c => c.String(nullable: false));
            DropColumn("dbo.Conference", "Delete");
        }
    }
}
