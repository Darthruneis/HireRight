namespace HireRight.EntityFramework.CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatingGeneratedIdForTables : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Company", "Id", c => c.Guid(nullable: false, identity: true, defaultValueSql: "NEWID()"));
            AlterColumn("dbo.CompanyLocation", "Id", c => c.Guid(nullable: false, identity: true, defaultValueSql: "NEWID()"));
            AlterColumn("dbo.Contact", "Id", c => c.Guid(nullable: false, identity: true, defaultValueSql: "NEWID()"));
            AlterColumn("dbo.Discount", "Id", c => c.Guid(nullable: false, identity: true, defaultValueSql: "NEWID()"));
            AlterColumn("dbo.Product", "Id", c => c.Guid(nullable: false, identity: true, defaultValueSql: "NEWID()"));
            AlterColumn("dbo.ScaleCategory", "Id", c => c.Guid(nullable: false, identity: true, defaultValueSql: "NEWID()"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Company", "Id", c => c.Guid(nullable: false, identity: true, defaultValueSql: "NEWSEQUENTIALID()"));
            AlterColumn("dbo.CompanyLocation", "Id", c => c.Guid(nullable: false, identity: true, defaultValueSql: "NEWSEQUENTIALID()"));
            AlterColumn("dbo.Contact", "Id", c => c.Guid(nullable: false, identity: true, defaultValueSql: "NEWSEQUENTIALID()"));
            AlterColumn("dbo.Discount", "Id", c => c.Guid(nullable: false, identity: true, defaultValueSql: "NEWSEQUENTIALID()"));
            AlterColumn("dbo.Product", "Id", c => c.Guid(nullable: false, identity: true, defaultValueSql: "NEWSEQUENTIALID()"));
            AlterColumn("dbo.ScaleCategory", "Id", c => c.Guid(nullable: false, identity: true, defaultValueSql: "NEWSEQUENTIALID()"));
        }
    }
}
