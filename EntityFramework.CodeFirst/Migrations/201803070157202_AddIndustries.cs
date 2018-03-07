namespace HireRight.EntityFramework.CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIndustries : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IndustryScaleCategory",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true, defaultValueSql: "NEWID()"),
                        IndustryId = c.Long(nullable: false),
                        CategoryId = c.Guid(nullable: false),
                        CreatedUtc = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ScaleCategory", t => t.CategoryId)
                .ForeignKey("dbo.Industry", t => t.IndustryId)
                .Index(t => t.IndustryId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Industry",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        CreatedUtc = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        RowGuid = c.Guid(nullable: false, defaultValueSql: "NEWID()"),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IndustryScaleCategory", "IndustryId", "dbo.Industry");
            DropForeignKey("dbo.IndustryScaleCategory", "CategoryId", "dbo.ScaleCategory");
            DropIndex("dbo.Industry", new[] { "Name" });
            DropIndex("dbo.IndustryScaleCategory", new[] { "CategoryId" });
            DropIndex("dbo.IndustryScaleCategory", new[] { "IndustryId" });
            DropTable("dbo.Industry");
            DropTable("dbo.IndustryScaleCategory");
        }
    }
}
