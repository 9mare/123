namespace ConferenceManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _12 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Reviewer", "ReviewerName", c => c.String(nullable: false));
            AlterColumn("dbo.Reviewer", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Reviewer", "Area", c => c.String(nullable: false));
            AlterColumn("dbo.Reviewer", "Instituition", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reviewer", "Instituition", c => c.String());
            AlterColumn("dbo.Reviewer", "Area", c => c.String());
            AlterColumn("dbo.Reviewer", "Email", c => c.String());
            AlterColumn("dbo.Reviewer", "ReviewerName", c => c.String());
        }
    }
}
