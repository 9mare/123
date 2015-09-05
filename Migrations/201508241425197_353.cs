namespace ConferenceManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _353 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Paper", "Marks", c => c.Int(nullable: false));
            AddColumn("dbo.ReviewItem", "OverallComments", c => c.String());
            AddColumn("dbo.ReviewItem", "AbstractSufficientlyInformative", c => c.Int(nullable: false));
            AddColumn("dbo.ReviewItem", "ClarityInThePresentationOfFindings", c => c.Int(nullable: false));
            AddColumn("dbo.ReviewItem", "MethodologyAppropriateToStudy", c => c.Int(nullable: false));
            AddColumn("dbo.ReviewItem", "ConclusionSupportedByDataAnalysis", c => c.Int(nullable: false));
            AddColumn("dbo.ReviewItem", "NoveltyOfFinding", c => c.Int(nullable: false));
            DropColumn("dbo.ReviewItem", "Name");
            DropColumn("dbo.ReviewItem", "ReviewTitleId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ReviewItem", "ReviewTitleId", c => c.Int(nullable: false));
            AddColumn("dbo.ReviewItem", "Name", c => c.String());
            DropColumn("dbo.ReviewItem", "NoveltyOfFinding");
            DropColumn("dbo.ReviewItem", "ConclusionSupportedByDataAnalysis");
            DropColumn("dbo.ReviewItem", "MethodologyAppropriateToStudy");
            DropColumn("dbo.ReviewItem", "ClarityInThePresentationOfFindings");
            DropColumn("dbo.ReviewItem", "AbstractSufficientlyInformative");
            DropColumn("dbo.ReviewItem", "OverallComments");
            DropColumn("dbo.Paper", "Marks");
        }
    }
}
