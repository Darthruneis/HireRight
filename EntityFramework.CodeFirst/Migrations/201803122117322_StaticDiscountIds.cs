using System.Data.Entity.Migrations;

namespace HireRight.Persistence.Migrations
{
    public partial class StaticDiscountIds : DbMigration
    {
        public override void Up()
        {
            Sql("DELETE FROM dbo.Discount");
            AddColumn("dbo.Discount", "StaticId", c => c.Long(nullable: false));
            CreateIndex("dbo.Discount", "StaticId", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Discount", new[] { "StaticId" });
            DropColumn("dbo.Discount", "StaticId");
        }
    }
}
