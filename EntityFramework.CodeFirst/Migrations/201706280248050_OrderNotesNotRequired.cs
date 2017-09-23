namespace HireRight.EntityFramework.CodeFirst.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class OrderNotesNotRequired : DbMigration
    {
        public override void Down()
        {
            AlterColumn("dbo.Order", "Notes", c => c.String(nullable: false));
        }

        public override void Up()
        {
            AlterColumn("dbo.Order", "Notes", c => c.String());
        }
    }
}