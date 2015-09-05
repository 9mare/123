namespace ConferenceManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1000 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmailTag",
                c => new
                    {
                        EmailTagsId = c.Int(nullable: false, identity: true),
                        Tags = c.String(),
                        Representation = c.String(),
                    })
                .PrimaryKey(t => t.EmailTagsId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.EmailTag");
        }
    }
}
