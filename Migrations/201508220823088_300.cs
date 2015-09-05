namespace ConferenceManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _300 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Admin", "Username", c => c.String(nullable: false));
            AlterColumn("dbo.Admin", "encryptedPassword", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Admin", "encryptedPassword", c => c.String());
            AlterColumn("dbo.Admin", "Username", c => c.String());
        }
    }
}
