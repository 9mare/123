namespace ConferenceManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _351 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ReviewItem", "Rating_RatingId", "dbo.Ratings");
            DropForeignKey("dbo.ReviewTitle", "Rating_RatingId", "dbo.Ratings");
            DropIndex("dbo.ReviewItem", new[] { "Rating_RatingId" });
            DropIndex("dbo.ReviewTitle", new[] { "Rating_RatingId" });
            DropColumn("dbo.ReviewItem", "Rating_RatingId");
            DropColumn("dbo.ReviewTitle", "RatingId");
            DropColumn("dbo.ReviewTitle", "Rating_RatingId");
            DropTable("dbo.Ratings");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        RatingId = c.Int(nullable: false, identity: true),
                        RatingNumber = c.String(),
                    })
                .PrimaryKey(t => t.RatingId);
            
            AddColumn("dbo.ReviewTitle", "Rating_RatingId", c => c.Int());
            AddColumn("dbo.ReviewTitle", "RatingId", c => c.String());
            AddColumn("dbo.ReviewItem", "Rating_RatingId", c => c.Int());
            CreateIndex("dbo.ReviewTitle", "Rating_RatingId");
            CreateIndex("dbo.ReviewItem", "Rating_RatingId");
            AddForeignKey("dbo.ReviewTitle", "Rating_RatingId", "dbo.Ratings", "RatingId");
            AddForeignKey("dbo.ReviewItem", "Rating_RatingId", "dbo.Ratings", "RatingId");
        }
    }
}
