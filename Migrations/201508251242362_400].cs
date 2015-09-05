namespace ConferenceManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _400 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Paper", "FullPaperFile", c => c.String());
            AddColumn("dbo.Paper", "CameraReadyPaperFile", c => c.String());
            AddColumn("dbo.Paper", "FullPaperSubmissionDate", c => c.String());
            AddColumn("dbo.Paper", "CameraReadyPaperSubmissionDate", c => c.String());
            AlterColumn("dbo.Paper", "AbstractSubmissionDate", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Paper", "AbstractSubmissionDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Paper", "CameraReadyPaperSubmissionDate");
            DropColumn("dbo.Paper", "FullPaperSubmissionDate");
            DropColumn("dbo.Paper", "CameraReadyPaperFile");
            DropColumn("dbo.Paper", "FullPaperFile");
        }
    }
}
