namespace ConferenceManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _100 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Conference", "ConferenceName", c => c.String(nullable: false));
            DropColumn("dbo.Conference", "ConferenceName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Conference", "ConferenceName", c => c.String(nullable: false));
            DropColumn("dbo.Conference", "ConferenceName");
        }
    }
}
