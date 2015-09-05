namespace ConferenceManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _221 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.AbstractFileFormat", new[] { "FontName_FontNameId" });
            RenameColumn(table: "dbo.AbstractFileFormat", name: "FontName_FontNameId", newName: "FontNameId");
            CreateTable(
                "dbo.Alignment",
                c => new
                    {
                        AlignmentId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.AlignmentId);
            
            AddColumn("dbo.AbstractFileFormat", "AlignmentId", c => c.Int(nullable: false));
            AddColumn("dbo.AbstractFileFormat", "FontSize", c => c.String());
            AddColumn("dbo.AbstractFileFormat", "Bold", c => c.Boolean(nullable: false));
            AddColumn("dbo.AbstractFileFormat", "Italic", c => c.Boolean(nullable: false));
            AddColumn("dbo.AbstractFileFormat", "Margin_Top", c => c.Single(nullable: false));
            AddColumn("dbo.AbstractFileFormat", "Margin_Bottom", c => c.Single(nullable: false));
            AddColumn("dbo.AbstractFileFormat", "Margin_Left", c => c.Single(nullable: false));
            AddColumn("dbo.AbstractFileFormat", "Margin_Right", c => c.Single(nullable: false));
            AlterColumn("dbo.AbstractFileFormat", "FontNameId", c => c.Int(nullable: false));
            CreateIndex("dbo.AbstractFileFormat", "AlignmentId");
            CreateIndex("dbo.AbstractFileFormat", "FontNameId");
            AddForeignKey("dbo.AbstractFileFormat", "AlignmentId", "dbo.Alignment", "AlignmentId");
            DropColumn("dbo.AbstractFileFormat", "Title_HorizontalAlignment");
            DropColumn("dbo.AbstractFileFormat", "Title_FontName");
            DropColumn("dbo.AbstractFileFormat", "Title_FontSize");
            DropColumn("dbo.AbstractFileFormat", "Title_Bold");
            DropColumn("dbo.AbstractFileFormat", "Title_Italic");
            DropColumn("dbo.AbstractFileFormat", "Name_HorizontalAlignment");
            DropColumn("dbo.AbstractFileFormat", "Name_FontSize");
            DropColumn("dbo.AbstractFileFormat", "Name_Bold");
            DropColumn("dbo.AbstractFileFormat", "Name_Italic");
            DropColumn("dbo.AbstractFileFormat", "Address_HorizontalAlignment");
            DropColumn("dbo.AbstractFileFormat", "Address_FontSize");
            DropColumn("dbo.AbstractFileFormat", "Address_Bold");
            DropColumn("dbo.AbstractFileFormat", "Address_Italic");
            DropColumn("dbo.AbstractFileFormat", "Email_HorizontalAlignment");
            DropColumn("dbo.AbstractFileFormat", "Email_FontSize");
            DropColumn("dbo.AbstractFileFormat", "Email_Bold");
            DropColumn("dbo.AbstractFileFormat", "Email_Italic");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AbstractFileFormat", "Email_Italic", c => c.Boolean(nullable: false));
            AddColumn("dbo.AbstractFileFormat", "Email_Bold", c => c.Boolean(nullable: false));
            AddColumn("dbo.AbstractFileFormat", "Email_FontSize", c => c.String(nullable: false));
            AddColumn("dbo.AbstractFileFormat", "Email_HorizontalAlignment", c => c.String(nullable: false));
            AddColumn("dbo.AbstractFileFormat", "Address_Italic", c => c.Boolean(nullable: false));
            AddColumn("dbo.AbstractFileFormat", "Address_Bold", c => c.Boolean(nullable: false));
            AddColumn("dbo.AbstractFileFormat", "Address_FontSize", c => c.String(nullable: false));
            AddColumn("dbo.AbstractFileFormat", "Address_HorizontalAlignment", c => c.String(nullable: false));
            AddColumn("dbo.AbstractFileFormat", "Name_Italic", c => c.Boolean(nullable: false));
            AddColumn("dbo.AbstractFileFormat", "Name_Bold", c => c.Boolean(nullable: false));
            AddColumn("dbo.AbstractFileFormat", "Name_FontSize", c => c.String(nullable: false));
            AddColumn("dbo.AbstractFileFormat", "Name_HorizontalAlignment", c => c.String(nullable: false));
            AddColumn("dbo.AbstractFileFormat", "Title_Italic", c => c.Boolean(nullable: false));
            AddColumn("dbo.AbstractFileFormat", "Title_Bold", c => c.Boolean(nullable: false));
            AddColumn("dbo.AbstractFileFormat", "Title_FontSize", c => c.String(nullable: false));
            AddColumn("dbo.AbstractFileFormat", "Title_FontName", c => c.Int(nullable: false));
            AddColumn("dbo.AbstractFileFormat", "Title_HorizontalAlignment", c => c.String(nullable: false));
            DropForeignKey("dbo.AbstractFileFormat", "AlignmentId", "dbo.Alignment");
            DropIndex("dbo.AbstractFileFormat", new[] { "FontNameId" });
            DropIndex("dbo.AbstractFileFormat", new[] { "AlignmentId" });
            AlterColumn("dbo.AbstractFileFormat", "FontNameId", c => c.Int());
            DropColumn("dbo.AbstractFileFormat", "Margin_Right");
            DropColumn("dbo.AbstractFileFormat", "Margin_Left");
            DropColumn("dbo.AbstractFileFormat", "Margin_Bottom");
            DropColumn("dbo.AbstractFileFormat", "Margin_Top");
            DropColumn("dbo.AbstractFileFormat", "Italic");
            DropColumn("dbo.AbstractFileFormat", "Bold");
            DropColumn("dbo.AbstractFileFormat", "FontSize");
            DropColumn("dbo.AbstractFileFormat", "AlignmentId");
            DropTable("dbo.Alignment");
            RenameColumn(table: "dbo.AbstractFileFormat", name: "FontNameId", newName: "FontName_FontNameId");
            CreateIndex("dbo.AbstractFileFormat", "FontName_FontNameId");
        }
    }
}
