namespace HireRight.EntityFramework.CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hardDriveChanged : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Company", "Address_City", c => c.String(nullable: false));
            AddColumn("dbo.Company", "Address_Country", c => c.String(nullable: false));
            AddColumn("dbo.Company", "Address_PostalCode", c => c.String(nullable: false, maxLength: 10));
            AddColumn("dbo.Company", "Address_State", c => c.String(nullable: false, maxLength: 2));
            AddColumn("dbo.Company", "Address_StreetAddress", c => c.String(nullable: false));
            AddColumn("dbo.Company", "Address_UnitNumber", c => c.String());
            DropColumn("dbo.Company", "BillingAddress_City");
            DropColumn("dbo.Company", "BillingAddress_Country");
            DropColumn("dbo.Company", "BillingAddress_PostalCode");
            DropColumn("dbo.Company", "BillingAddress_State");
            DropColumn("dbo.Company", "BillingAddress_StreetAddress");
            DropColumn("dbo.Company", "BillingAddress_UnitNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Company", "BillingAddress_UnitNumber", c => c.String());
            AddColumn("dbo.Company", "BillingAddress_StreetAddress", c => c.String(nullable: false));
            AddColumn("dbo.Company", "BillingAddress_State", c => c.String(nullable: false, maxLength: 2));
            AddColumn("dbo.Company", "BillingAddress_PostalCode", c => c.String(nullable: false, maxLength: 10));
            AddColumn("dbo.Company", "BillingAddress_Country", c => c.String(nullable: false));
            AddColumn("dbo.Company", "BillingAddress_City", c => c.String(nullable: false));
            DropColumn("dbo.Company", "Address_UnitNumber");
            DropColumn("dbo.Company", "Address_StreetAddress");
            DropColumn("dbo.Company", "Address_State");
            DropColumn("dbo.Company", "Address_PostalCode");
            DropColumn("dbo.Company", "Address_Country");
            DropColumn("dbo.Company", "Address_City");
        }
    }
}
