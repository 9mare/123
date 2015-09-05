namespace ConferenceManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _502 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Paper", "AbstractTotalNumberOfPages", c => c.Int(nullable: false));
            AddColumn("dbo.Paper", "FullPaperTotalNumberOfPages", c => c.Int(nullable: false));
            DropColumn("dbo.Paper", "Co_Author");
            DropColumn("dbo.Paper", "TotalNumberOfPages");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Paper", "TotalNumberOfPages", c => c.Int(nullable: false));
            AddColumn("dbo.Paper", "Co_Author", c => c.String());
            DropColumn("dbo.Paper", "FullPaperTotalNumberOfPages");
            DropColumn("dbo.Paper", "AbstractTotalNumberOfPages");
        }
    }
}
