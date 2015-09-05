namespace ConferenceManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1004 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Conference", "Fax", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Conference", "Fax");
        }
    }
}
