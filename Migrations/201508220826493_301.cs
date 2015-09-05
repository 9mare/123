namespace ConferenceManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _301 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Admin", "encryptedPassword", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Admin", "encryptedPassword", c => c.String(nullable: false));
        }
    }
}
