using System.Data.Entity.Migrations;

namespace HireRight.Persistence.Migrations
{
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