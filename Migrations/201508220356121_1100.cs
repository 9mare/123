namespace ConferenceManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1100 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AbstractFileFormat", "FontSize", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AbstractFileFormat", "FontSize", c => c.String());
        }
    }
}
