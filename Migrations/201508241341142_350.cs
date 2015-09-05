namespace ConferenceManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _350 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        RatingId = c.Int(nullable: false, identity: true),
                        RatingNumber = c.String(),
                    })
                .PrimaryKey(t => t.RatingId);
            
            AddColumn("dbo.ReviewItem", "Rating_RatingId", c => c.Int());
            AddColumn("dbo.ReviewTitle", "RatingId", c => c.String());
            AddColumn("dbo.ReviewTitle", "Rating_RatingId", c => c.Int());
            CreateIndex("dbo.ReviewItem", "Rating_RatingId");
            CreateIndex("dbo.ReviewTitle", "Rating_RatingId");
            AddForeignKey("dbo.ReviewItem", "Rating_RatingId", "dbo.Ratings", "RatingId");
            AddForeignKey("dbo.ReviewTitle", "Rating_RatingId", "dbo.Ratings", "RatingId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReviewTitle", "Rating_RatingId", "dbo.Ratings");
            DropForeignKey("dbo.ReviewItem", "Rating_RatingId", "dbo.Ratings");
            DropIndex("dbo.ReviewTitle", new[] { "Rating_RatingId" });
            DropIndex("dbo.ReviewItem", new[] { "Rating_RatingId" });
            DropColumn("dbo.ReviewTitle", "Rating_RatingId");
            DropColumn("dbo.ReviewTitle", "RatingId");
            DropColumn("dbo.ReviewItem", "Rating_RatingId");
            DropTable("dbo.Ratings");
        }
    }
}
