using System.Data.Entity.Migrations;

namespace HireRight.Persistence.Migrations
{
    public partial class initializeDatabase : DbMigration
    {
        public override void Down()
        {
            DropForeignKey("dbo.Order", "ProductId", "dbo.Product");
            DropForeignKey("dbo.Discount", "ProductId", "dbo.Product");
            DropForeignKey("dbo.Order", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.CompanyLocation", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.Contact", "CompanyId", "dbo.Company");
            DropIndex("dbo.Discount", new[] { "ProductId" });
            DropIndex("dbo.Order", new[] { "ProductId" });
            DropIndex("dbo.Order", new[] { "CompanyId" });
            DropIndex("dbo.CompanyLocation", new[] { "CompanyId" });
            DropIndex("dbo.Contact", new[] { "CompanyId" });
            DropTable("dbo.Discount");
            DropTable("dbo.Product");
            DropTable("dbo.Order");
            DropTable("dbo.CompanyLocation");
            DropTable("dbo.Contact");
            DropTable("dbo.Company");
            DropTable("dbo.ScaleCategory");
        }

        public override void Up()
        {
            CreateTable(
                "dbo.ScaleCategory",
                c => new
                {
                    Id = c.Guid(nullable: false, identity: true, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = c.String(nullable: false),
                    Title = c.String(nullable: false),
                    CreatedUtc = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                    TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Company",
                c => new
                {
                    Id = c.Guid(nullable: false, identity: true, defaultValueSql: "NEWSEQUENTIALID()"),
                    City = c.String(nullable: false),
                    Country = c.String(nullable: false),
                    PostalCode = c.String(nullable: false, maxLength: 10),
                    State = c.String(nullable: false, maxLength: 2),
                    StreetAddress = c.String(nullable: false),
                    UnitNumber = c.String(),
                    Name = c.String(nullable: false),
                    Notes = c.String(),
                    CreatedUtc = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                    TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Contact",
                c => new
                {
                    Id = c.Guid(nullable: false, identity: true, defaultValueSql: "NEWSEQUENTIALID()"),
                    City = c.String(nullable: false),
                    Country = c.String(nullable: false),
                    PostalCode = c.String(nullable: false, maxLength: 10),
                    State = c.String(nullable: false, maxLength: 2),
                    StreetAddress = c.String(nullable: false),
                    UnitNumber = c.String(),
                    CellNumber = c.String(nullable: false),
                    CompanyId = c.Guid(nullable: false),
                    Email = c.String(nullable: false),
                    IsAdmin = c.Boolean(nullable: false),
                    IsPrimary = c.Boolean(nullable: false),
                    Name = c.String(nullable: false),
                    OfficeNumber = c.String(nullable: false),
                    CreatedUtc = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                    TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .Index(t => t.CompanyId);

            CreateTable(
                "dbo.CompanyLocation",
                c => new
                {
                    Id = c.Guid(nullable: false, identity: true, defaultValueSql: "NEWSEQUENTIALID()"),
                    City = c.String(nullable: false),
                    Country = c.String(nullable: false),
                    PostalCode = c.String(nullable: false, maxLength: 10),
                    State = c.String(nullable: false, maxLength: 2),
                    StreetAddress = c.String(nullable: false),
                    UnitNumber = c.String(),
                    CompanyId = c.Guid(nullable: false),
                    Description = c.String(nullable: false),
                    Label = c.String(nullable: false),
                    CreatedUtc = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                    TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .Index(t => t.CompanyId);

            CreateTable(
                "dbo.Order",
                c => new
                {
                    Id = c.Guid(nullable: false, identity: true, defaultValueSql: "NEWSEQUENTIALID()"),
                    CompanyId = c.Guid(nullable: false),
                    Completed = c.DateTime(),
                    Notes = c.String(nullable: false),
                    ProductId = c.Guid(nullable: false),
                    Quantity = c.Int(nullable: false),
                    Status = c.Int(nullable: false),
                    CreatedUtc = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                    TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .ForeignKey("dbo.Product", t => t.ProductId)
                .Index(t => t.CompanyId)
                .Index(t => t.ProductId);

            CreateTable(
                "dbo.Product",
                c => new
                {
                    Id = c.Guid(nullable: false, identity: true, defaultValueSql: "NEWSEQUENTIALID()"),
                    Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Title = c.String(nullable: false),
                    CreatedUtc = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                    TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Discount",
                c => new
                {
                    Id = c.Guid(nullable: false, identity: true, defaultValueSql: "NEWSEQUENTIALID()"),
                    Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    IsPercent = c.Boolean(nullable: false),
                    ProductId = c.Guid(nullable: false),
                    Threshold = c.Int(nullable: false),
                    CreatedUtc = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                    TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.ProductId)
                .Index(t => t.ProductId);
        }
    }
}