namespace ConferenceManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Conference", "LinkDirectory");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Conference", "LinkDirectory", c => c.String(nullable: false));
        }
    }
}
