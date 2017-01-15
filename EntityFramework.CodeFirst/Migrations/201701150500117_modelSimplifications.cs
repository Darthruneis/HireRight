namespace HireRight.EntityFramework.CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modelSimplifications : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Client", "AccountId", "dbo.Account");
            DropForeignKey("dbo.Contact", "ClientId", "dbo.Client");
            DropForeignKey("dbo.Client", "Company_Id", "dbo.Company");
            DropForeignKey("dbo.Client", "AdminContactId", "dbo.Contact");
            DropForeignKey("dbo.Client", "PrimaryContactId", "dbo.Contact");
            DropForeignKey("dbo.Account", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.Order", "Account_Id", "dbo.Account");
            DropIndex("dbo.Account", new[] { "CompanyId" });
            DropIndex("dbo.Client", new[] { "AccountId" });
            DropIndex("dbo.Client", new[] { "AdminContactId" });
            DropIndex("dbo.Client", new[] { "PrimaryContactId" });
            DropIndex("dbo.Client", new[] { "Company_Id" });
            DropIndex("dbo.Contact", new[] { "ClientId" });
            DropIndex("dbo.Order", new[] { "Account_Id" });
            AddColumn("dbo.Company", "Notes", c => c.String());
            AlterColumn("dbo.CompanyLocation", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.CompanyLocation", "Label", c => c.String(nullable: false));
            DropColumn("dbo.Contact", "ClientId");
            DropColumn("dbo.Order", "Account_Id");
            DropTable("dbo.Account");
            DropTable("dbo.Client");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Client",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        AccountId = c.Guid(nullable: false),
                        AdminContactId = c.Guid(nullable: false),
                        PrimaryContactId = c.Guid(nullable: false),
                        CreatedUtc = c.DateTime(nullable: false),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Company_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Account",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        CompanyId = c.Guid(nullable: false),
                        Notes = c.String(nullable: false),
                        CreatedUtc = c.DateTime(nullable: false),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Order", "Account_Id", c => c.Guid());
            AddColumn("dbo.Contact", "ClientId", c => c.Guid(nullable: false));
            AlterColumn("dbo.CompanyLocation", "Label", c => c.String());
            AlterColumn("dbo.CompanyLocation", "Description", c => c.String());
            DropColumn("dbo.Company", "Notes");
            CreateIndex("dbo.Order", "Account_Id");
            CreateIndex("dbo.Contact", "ClientId");
            CreateIndex("dbo.Client", "Company_Id");
            CreateIndex("dbo.Client", "PrimaryContactId");
            CreateIndex("dbo.Client", "AdminContactId");
            CreateIndex("dbo.Client", "AccountId");
            CreateIndex("dbo.Account", "CompanyId");
            AddForeignKey("dbo.Order", "Account_Id", "dbo.Account", "Id");
            AddForeignKey("dbo.Account", "CompanyId", "dbo.Company", "Id");
            AddForeignKey("dbo.Client", "PrimaryContactId", "dbo.Contact", "Id");
            AddForeignKey("dbo.Client", "AdminContactId", "dbo.Contact", "Id");
            AddForeignKey("dbo.Client", "Company_Id", "dbo.Company", "Id");
            AddForeignKey("dbo.Contact", "ClientId", "dbo.Client", "Id");
            AddForeignKey("dbo.Client", "AccountId", "dbo.Account", "Id");
        }
    }
}
