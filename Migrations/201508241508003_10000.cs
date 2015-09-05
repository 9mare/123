namespace ConferenceManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _10000 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BreakTimes",
                c => new
                    {
                        BreakTimeId = c.Int(nullable: false, identity: true),
                        StartTime = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        EventDate = c.DateTime(nullable: false),
                        Task = c.String(),
                    })
                .PrimaryKey(t => t.BreakTimeId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BreakTimes");
        }
    }
}
