namespace HireRight.EntityFramework.CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsActiveFlagOnAllPocos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Assessment",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 400),
                        Duration = c.Int(),
                        IsTimed = c.Boolean(nullable: false),
                        AssessmentTypeId = c.Long(nullable: false),
                        CreatedUtc = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        RowGuid = c.Guid(nullable: false, defaultValueSql: "NEWID()"),
                        IsActive = c.Boolean(nullable: false, defaultValue: true),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AssessmentType", t => t.AssessmentTypeId)
                .Index(t => t.Title, unique: true)
                .Index(t => t.AssessmentTypeId);
            
            CreateTable(
                "dbo.AssessmentType",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 120),
                        StaticId = c.Long(nullable: false),
                        CreatedUtc = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        RowGuid = c.Guid(nullable: false, defaultValueSql: "NEWID()"),
                        IsActive = c.Boolean(nullable: false, defaultValue: true),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true)
                .Index(t => t.StaticId, unique: true);
            
            CreateTable(
                "dbo.AssessmentScaleCategoryBinder",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AssessmentId = c.Long(nullable: false),
                        ScaleCategoryId = c.Long(nullable: false),
                        CreatedUtc = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        RowGuid = c.Guid(nullable: false, defaultValueSql: "NEWID()"),
                        IsActive = c.Boolean(nullable: false, defaultValue: true),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Assessment", t => t.AssessmentId)
                .ForeignKey("dbo.ScaleCategory", t => t.ScaleCategoryId)
                .Index(t => t.AssessmentId)
                .Index(t => t.ScaleCategoryId);
            
            CreateTable(
                "dbo.IndustryAssessmentBinder",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AssessmentId = c.Long(nullable: false),
                        IndustryId = c.Long(nullable: false),
                        CreatedUtc = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        RowGuid = c.Guid(nullable: false, defaultValueSql: "NEWID()"),
                        IsActive = c.Boolean(nullable: false, defaultValue: true),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Assessment", t => t.AssessmentId)
                .ForeignKey("dbo.Industry", t => t.IndustryId)
                .Index(t => t.AssessmentId)
                .Index(t => t.IndustryId);
            
            AddColumn("dbo.ScaleCategory", "IsActive", c => c.Boolean(nullable: false, defaultValue: true));
            AddColumn("dbo.IndustryScaleCategory", "IsActive", c => c.Boolean(nullable: false, defaultValue: true));
            AddColumn("dbo.Industry", "IsActive", c => c.Boolean(nullable: false, defaultValue: true));
            AddColumn("dbo.Company", "IsActive", c => c.Boolean(nullable: false, defaultValue: true));
            AddColumn("dbo.Contact", "IsActive", c => c.Boolean(nullable: false, defaultValue: true));
            AddColumn("dbo.CompanyLocation", "IsActive", c => c.Boolean(nullable: false, defaultValue: true));
            AddColumn("dbo.Order", "IsActive", c => c.Boolean(nullable: false, defaultValue: true));
            AddColumn("dbo.Product", "IsActive", c => c.Boolean(nullable: false, defaultValue: true));
            AddColumn("dbo.Discount", "IsActive", c => c.Boolean(nullable: false, defaultValue: true));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IndustryAssessmentBinder", "IndustryId", "dbo.Industry");
            DropForeignKey("dbo.IndustryAssessmentBinder", "AssessmentId", "dbo.Assessment");
            DropForeignKey("dbo.AssessmentScaleCategoryBinder", "ScaleCategoryId", "dbo.ScaleCategory");
            DropForeignKey("dbo.AssessmentScaleCategoryBinder", "AssessmentId", "dbo.Assessment");
            DropForeignKey("dbo.Assessment", "AssessmentTypeId", "dbo.AssessmentType");
            DropIndex("dbo.IndustryAssessmentBinder", new[] { "IndustryId" });
            DropIndex("dbo.IndustryAssessmentBinder", new[] { "AssessmentId" });
            DropIndex("dbo.AssessmentScaleCategoryBinder", new[] { "ScaleCategoryId" });
            DropIndex("dbo.AssessmentScaleCategoryBinder", new[] { "AssessmentId" });
            DropIndex("dbo.AssessmentType", new[] { "StaticId" });
            DropIndex("dbo.AssessmentType", new[] { "Name" });
            DropIndex("dbo.Assessment", new[] { "AssessmentTypeId" });
            DropIndex("dbo.Assessment", new[] { "Title" });
            DropColumn("dbo.Discount", "IsActive");
            DropColumn("dbo.Product", "IsActive");
            DropColumn("dbo.Order", "IsActive");
            DropColumn("dbo.CompanyLocation", "IsActive");
            DropColumn("dbo.Contact", "IsActive");
            DropColumn("dbo.Company", "IsActive");
            DropColumn("dbo.Industry", "IsActive");
            DropColumn("dbo.IndustryScaleCategory", "IsActive");
            DropColumn("dbo.ScaleCategory", "IsActive");
            DropTable("dbo.IndustryAssessmentBinder");
            DropTable("dbo.AssessmentScaleCategoryBinder");
            DropTable("dbo.AssessmentType");
            DropTable("dbo.Assessment");
        }
    }
}
