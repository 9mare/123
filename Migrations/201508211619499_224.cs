namespace ConferenceManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _224 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AbstractFileFormat", "Margin_Top", c => c.Double(nullable: false));
            AlterColumn("dbo.AbstractFileFormat", "Margin_Bottom", c => c.Double(nullable: false));
            AlterColumn("dbo.AbstractFileFormat", "Margin_Left", c => c.Double(nullable: false));
            AlterColumn("dbo.AbstractFileFormat", "Margin_Right", c => c.Double(nullable: false));
            AlterColumn("dbo.AbstractFileFormat", "LineSpacing", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AbstractFileFormat", "LineSpacing", c => c.Single(nullable: false));
            AlterColumn("dbo.AbstractFileFormat", "Margin_Right", c => c.Single(nullable: false));
            AlterColumn("dbo.AbstractFileFormat", "Margin_Left", c => c.Single(nullable: false));
            AlterColumn("dbo.AbstractFileFormat", "Margin_Bottom", c => c.Single(nullable: false));
            AlterColumn("dbo.AbstractFileFormat", "Margin_Top", c => c.Single(nullable: false));
        }
    }
}
