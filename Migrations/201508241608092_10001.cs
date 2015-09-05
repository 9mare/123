namespace ConferenceManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _10001 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BreakTimes", "TopicId", c => c.Int(nullable: false));
            CreateIndex("dbo.BreakTimes", "TopicId");
            AddForeignKey("dbo.BreakTimes", "TopicId", "dbo.Topic", "TopicId");
            DropColumn("dbo.BreakTimes", "EventDate");
            DropColumn("dbo.BreakTimes", "Task");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BreakTimes", "Task", c => c.String());
            AddColumn("dbo.BreakTimes", "EventDate", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.BreakTimes", "TopicId", "dbo.Topic");
            DropIndex("dbo.BreakTimes", new[] { "TopicId" });
            DropColumn("dbo.BreakTimes", "TopicId");
        }
    }
}
