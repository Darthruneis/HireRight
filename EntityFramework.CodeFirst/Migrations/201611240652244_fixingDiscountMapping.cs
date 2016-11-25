namespace HireRight.EntityFramework.CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixingDiscountMapping : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Discount", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Discount", "Threshold", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Discount", "Threshold");
            DropColumn("dbo.Discount", "Amount");
        }
    }
}
