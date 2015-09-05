namespace ConferenceManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _223 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AbstractFileFormat", "Bold");
            DropColumn("dbo.AbstractFileFormat", "Italic");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AbstractFileFormat", "Italic", c => c.Boolean(nullable: false));
            AddColumn("dbo.AbstractFileFormat", "Bold", c => c.Boolean(nullable: false));
        }
    }
}
