namespace HireRight.EntityFramework.CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StaticIdsForCategories : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.IndustryScaleCategory", "CategoryId", "dbo.ScaleCategory");
            DropPrimaryKey("dbo.ScaleCategory");
            AlterColumn("dbo.ScaleCategory", "Id", c => c.Long(nullable: false));
            AddPrimaryKey("dbo.ScaleCategory", "Id");
            AddForeignKey("dbo.IndustryScaleCategory", "CategoryId", "dbo.ScaleCategory", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IndustryScaleCategory", "CategoryId", "dbo.ScaleCategory");
            DropPrimaryKey("dbo.ScaleCategory");
            AlterColumn("dbo.ScaleCategory", "Id", c => c.Long(nullable: false, identity: true));
            AddPrimaryKey("dbo.ScaleCategory", "Id");
            AddForeignKey("dbo.IndustryScaleCategory", "CategoryId", "dbo.ScaleCategory", "Id");
        }
    }
}
