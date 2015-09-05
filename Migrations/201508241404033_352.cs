namespace ConferenceManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _352 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ReviewTitle", "ConferenceId", "dbo.Conference");
            DropForeignKey("dbo.ReviewItem", "ReviewTitleId", "dbo.ReviewTitle");
            DropForeignKey("dbo.ReviewTitle", "ReviewTypeId", "dbo.ReviewType");
            DropIndex("dbo.ReviewItem", new[] { "ReviewTitleId" });
            DropIndex("dbo.ReviewTitle", new[] { "ReviewTypeId" });
            DropIndex("dbo.ReviewTitle", new[] { "ConferenceId" });
            DropTable("dbo.ReviewTitle");
            DropTable("dbo.ReviewType");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ReviewType",
                c => new
                    {
                        ReviewTypeId = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        HtmlElement = c.String(),
                    })
                .PrimaryKey(t => t.ReviewTypeId);
            
            CreateTable(
                "dbo.ReviewTitle",
                c => new
                    {
                        ReviewTitleId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        ReviewTypeId = c.Int(nullable: false),
                        ConferenceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReviewTitleId);
            
            CreateIndex("dbo.ReviewTitle", "ConferenceId");
            CreateIndex("dbo.ReviewTitle", "ReviewTypeId");
            CreateIndex("dbo.ReviewItem", "ReviewTitleId");
            AddForeignKey("dbo.ReviewTitle", "ReviewTypeId", "dbo.ReviewType", "ReviewTypeId");
            AddForeignKey("dbo.ReviewItem", "ReviewTitleId", "dbo.ReviewTitle", "ReviewTitleId");
            AddForeignKey("dbo.ReviewTitle", "ConferenceId", "dbo.Conference", "ConferenceId");
        }
    }
}
