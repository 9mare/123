namespace ConferenceManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _501 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Paper", "Marks");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Paper", "Marks", c => c.Int(nullable: false));
        }
    }
}
