namespace ConferenceManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _10 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PaymentStatus",
                c => new
                    {
                        PaymentStatusId = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.PaymentStatusId);
            
            AddColumn("dbo.Attendee", "RegDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Attendee", "PaymentStatusId", c => c.Int(nullable: false));
            AddColumn("dbo.Attendee", "PaymentAmount", c => c.Int(nullable: false));
            AddColumn("dbo.Attendee", "PaymentDetail", c => c.String());
            AddColumn("dbo.Attendee", "ReceiptNumber", c => c.String());
            CreateIndex("dbo.Attendee", "PaymentStatusId");
            AddForeignKey("dbo.Attendee", "PaymentStatusId", "dbo.PaymentStatus", "PaymentStatusId");
            DropColumn("dbo.User", "RegDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User", "RegDate", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.Attendee", "PaymentStatusId", "dbo.PaymentStatus");
            DropIndex("dbo.Attendee", new[] { "PaymentStatusId" });
            DropColumn("dbo.Attendee", "ReceiptNumber");
            DropColumn("dbo.Attendee", "PaymentDetail");
            DropColumn("dbo.Attendee", "PaymentAmount");
            DropColumn("dbo.Attendee", "PaymentStatusId");
            DropColumn("dbo.Attendee", "RegDate");
            DropTable("dbo.PaymentStatus");
        }
    }
}
