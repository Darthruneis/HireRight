namespace HireRight.EntityFramework.CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Account",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true, defaultValueSql: "NEWSEQUENTIALID()"),
                        CompanyId = c.Guid(nullable: false),
                        Notes = c.String(nullable: false),
                        CreatedUtc = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.Client",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true, defaultValueSql: "NEWSEQUENTIALID()"),
                        AccountId = c.Guid(nullable: false),
                        AdminContactId = c.Guid(nullable: false),
                        PrimaryContactId = c.Guid(nullable: false),
                        CreatedUtc = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Company_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.AccountId)
                .ForeignKey("dbo.Company", t => t.Company_Id)
                .ForeignKey("dbo.Contact", t => t.AdminContactId)
                .ForeignKey("dbo.Contact", t => t.PrimaryContactId)
                .Index(t => t.AccountId)
                .Index(t => t.AdminContactId)
                .Index(t => t.PrimaryContactId)
                .Index(t => t.Company_Id);
            
            CreateTable(
                "dbo.Contact",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true, defaultValueSql: "NEWSEQUENTIALID()"),
                        Address_City = c.String(nullable: false),
                        Address_Country = c.String(nullable: false),
                        Address_PostalCode = c.String(nullable: false, maxLength: 10),
                        Address_State = c.String(nullable: false, maxLength: 2),
                        Address_StreetAddress = c.String(nullable: false),
                        Address_UnitNumber = c.String(),
                        CellNumber = c.String(nullable: false),
                        ClientId = c.Guid(nullable: false),
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
                .ForeignKey("dbo.Client", t => t.ClientId)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .Index(t => t.ClientId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.Company",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true, defaultValueSql: "NEWSEQUENTIALID()"),
                        BillingAddress_City = c.String(nullable: false),
                        BillingAddress_Country = c.String(nullable: false),
                        BillingAddress_PostalCode = c.String(nullable: false, maxLength: 10),
                        BillingAddress_State = c.String(nullable: false, maxLength: 2),
                        BillingAddress_StreetAddress = c.String(nullable: false),
                        BillingAddress_UnitNumber = c.String(),
                        Name = c.String(nullable: false),
                        CreatedUtc = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CompanyLocation",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true, defaultValueSql: "NEWSEQUENTIALID()"),
                        Address_City = c.String(nullable: false),
                        Address_Country = c.String(nullable: false),
                        Address_PostalCode = c.String(nullable: false, maxLength: 10),
                        Address_State = c.String(nullable: false, maxLength: 2),
                        Address_StreetAddress = c.String(nullable: false),
                        Address_UnitNumber = c.String(),
                        CompanyId = c.Guid(nullable: false),
                        Description = c.String(),
                        Label = c.String(),
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
                        Account_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .ForeignKey("dbo.Product", t => t.ProductId)
                .ForeignKey("dbo.Account", t => t.Account_Id)
                .Index(t => t.CompanyId)
                .Index(t => t.ProductId)
                .Index(t => t.Account_Id);
            
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
                        IsPercent = c.Boolean(nullable: false),
                        ProductId = c.Guid(nullable: false),
                        CreatedUtc = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.ProductId)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Order", "Account_Id", "dbo.Account");
            DropForeignKey("dbo.Order", "ProductId", "dbo.Product");
            DropForeignKey("dbo.Discount", "ProductId", "dbo.Product");
            DropForeignKey("dbo.Order", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.Account", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.Client", "PrimaryContactId", "dbo.Contact");
            DropForeignKey("dbo.Client", "AdminContactId", "dbo.Contact");
            DropForeignKey("dbo.Contact", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.CompanyLocation", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.Client", "Company_Id", "dbo.Company");
            DropForeignKey("dbo.Contact", "ClientId", "dbo.Client");
            DropForeignKey("dbo.Client", "AccountId", "dbo.Account");
            DropIndex("dbo.Discount", new[] { "ProductId" });
            DropIndex("dbo.Order", new[] { "Account_Id" });
            DropIndex("dbo.Order", new[] { "ProductId" });
            DropIndex("dbo.Order", new[] { "CompanyId" });
            DropIndex("dbo.CompanyLocation", new[] { "CompanyId" });
            DropIndex("dbo.Contact", new[] { "CompanyId" });
            DropIndex("dbo.Contact", new[] { "ClientId" });
            DropIndex("dbo.Client", new[] { "Company_Id" });
            DropIndex("dbo.Client", new[] { "PrimaryContactId" });
            DropIndex("dbo.Client", new[] { "AdminContactId" });
            DropIndex("dbo.Client", new[] { "AccountId" });
            DropIndex("dbo.Account", new[] { "CompanyId" });
            DropTable("dbo.Discount");
            DropTable("dbo.Product");
            DropTable("dbo.Order");
            DropTable("dbo.CompanyLocation");
            DropTable("dbo.Company");
            DropTable("dbo.Contact");
            DropTable("dbo.Client");
            DropTable("dbo.Account");
        }
    }
}
