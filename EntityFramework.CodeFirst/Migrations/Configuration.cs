using HireRight.EntityFramework.CodeFirst.Database_Context;
using HireRight.EntityFramework.CodeFirst.Seeds;
using System.Data.Entity.Migrations;

namespace HireRight.EntityFramework.CodeFirst.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<HireRightDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = true;
            CodeGenerator = new BaseMigrationCodeGenerator();
        }

        protected override void Seed(HireRightDbContext context)
        {
            ProductsSeed.Seed().ForEach(product => context.Products.AddOrUpdate(product));
            ScaleCategorySeed.Seed().ForEach(category => context.Categories.AddOrUpdate(category));

            context.SaveChanges();
        }
    }
}