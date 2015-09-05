namespace ConferenceManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _412 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Paper", "AbstractSubmissionNotification");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Paper", "AbstractSubmissionNotification", c => c.Int(nullable: false));
        }
    }
}
