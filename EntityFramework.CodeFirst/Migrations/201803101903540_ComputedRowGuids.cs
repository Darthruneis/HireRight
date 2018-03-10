namespace HireRight.EntityFramework.CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ComputedRowGuids : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ScaleCategory", "RowGuid", c => c.Guid(nullable: false));
            AlterColumn("dbo.IndustryScaleCategory", "RowGuid", c => c.Guid(nullable: false));
            AlterColumn("dbo.Industry", "RowGuid", c => c.Guid(nullable: false));
            AlterColumn("dbo.Company", "RowGuid", c => c.Guid(nullable: false));
            AlterColumn("dbo.Contact", "RowGuid", c => c.Guid(nullable: false));
            AlterColumn("dbo.CompanyLocation", "RowGuid", c => c.Guid(nullable: false));
            AlterColumn("dbo.Order", "RowGuid", c => c.Guid(nullable: false));
            AlterColumn("dbo.Product", "RowGuid", c => c.Guid(nullable: false));
            AlterColumn("dbo.Discount", "RowGuid", c => c.Guid(nullable: false));
            Sql($"UPDATE dbo.ScaleCategory SET RowGuid = NEWID() WHERE RowGuid = '{Guid.Empty}'");
            Sql($"UPDATE dbo.IndustryScaleCategory SET RowGuid = NEWID() WHERE RowGuid = '{Guid.Empty}'");
            Sql($"UPDATE dbo.Industry SET RowGuid = NEWID() WHERE RowGuid = '{Guid.Empty}'");
            Sql($"UPDATE dbo.Company SET RowGuid = NEWID() WHERE RowGuid = '{Guid.Empty}'");
            Sql($"UPDATE dbo.Contact SET RowGuid = NEWID() WHERE RowGuid = '{Guid.Empty}'");
            Sql($"UPDATE dbo.CompanyLocation SET RowGuid = NEWID() WHERE RowGuid = '{Guid.Empty}'");
            Sql($"UPDATE dbo.[Order] SET RowGuid = NEWID() WHERE RowGuid = '{Guid.Empty}'");
            Sql($"UPDATE dbo.Product SET RowGuid = NEWID() WHERE RowGuid = '{Guid.Empty}'");
            Sql($"UPDATE dbo.Discount SET RowGuid = NEWID() WHERE RowGuid = '{Guid.Empty}'");
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Discount", "RowGuid", c => c.Guid(nullable: false));
            AlterColumn("dbo.Product", "RowGuid", c => c.Guid(nullable: false));
            AlterColumn("dbo.Order", "RowGuid", c => c.Guid(nullable: false));
            AlterColumn("dbo.CompanyLocation", "RowGuid", c => c.Guid(nullable: false));
            AlterColumn("dbo.Contact", "RowGuid", c => c.Guid(nullable: false));
            AlterColumn("dbo.Company", "RowGuid", c => c.Guid(nullable: false));
            AlterColumn("dbo.Industry", "RowGuid", c => c.Guid(nullable: false));
            AlterColumn("dbo.IndustryScaleCategory", "RowGuid", c => c.Guid(nullable: false));
            AlterColumn("dbo.ScaleCategory", "RowGuid", c => c.Guid(nullable: false));
        }
    }
}
