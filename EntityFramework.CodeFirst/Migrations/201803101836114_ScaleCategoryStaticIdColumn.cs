namespace HireRight.EntityFramework.CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ScaleCategoryStaticIdColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ScaleCategory", "StaticId", c => c.Long(nullable: false));
            CreateIndex("dbo.ScaleCategory", "StaticId", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.ScaleCategory", new[] { "StaticId" });
            DropColumn("dbo.ScaleCategory", "StaticId");
        }
    }
}
