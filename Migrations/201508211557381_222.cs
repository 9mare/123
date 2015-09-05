namespace ConferenceManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _222 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FontName", "Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FontName", "Name", c => c.Int(nullable: false));
        }
    }
}
