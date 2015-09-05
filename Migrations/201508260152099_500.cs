namespace ConferenceManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _500 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Paper", "AbstractPaperStatus", c => c.Int(nullable: false));
            AddColumn("dbo.Paper", "FullPaperStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Paper", "FullPaperStatus");
            DropColumn("dbo.Paper", "AbstractPaperStatus");
        }
    }
}
