namespace ConferenceManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _411 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BreakTimes", "TopicId", "dbo.Topic");
            DropIndex("dbo.BreakTimes", new[] { "TopicId" });
            CreateTable(
                "dbo.Schedule",
                c => new
                    {
                        ScheduleId = c.Int(nullable: false, identity: true),
                        StartTime = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Venue = c.String(),
                        TopicId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ScheduleId)
                .ForeignKey("dbo.Topic", t => t.TopicId)
                .Index(t => t.TopicId);
            
            DropTable("dbo.BreakTimes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.BreakTimes",
                c => new
                    {
                        BreakTimeId = c.Int(nullable: false, identity: true),
                        TopicId = c.Int(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.BreakTimeId);
            
            DropForeignKey("dbo.Schedule", "TopicId", "dbo.Topic");
            DropIndex("dbo.Schedule", new[] { "TopicId" });
            DropTable("dbo.Schedule");
            CreateIndex("dbo.BreakTimes", "TopicId");
            AddForeignKey("dbo.BreakTimes", "TopicId", "dbo.Topic", "TopicId");
        }
    }
}
