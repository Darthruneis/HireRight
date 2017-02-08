using HireRight.EntityFramework.CodeFirst.Database_Context;
using HireRight.EntityFramework.CodeFirst.Seeds;
using System.Data.Entity.Migrations;
using System.Linq;
using HireRight.EntityFramework.CodeFirst.Models.CompanyAggregate;

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
            foreach (Product product in ProductsSeed.Seed())
                if (!context.Products.Any(x => x.Title == product.Title && x.Price == product.Price && x.Discounts.Count == product.Discounts.Count))
                    context.Products.AddOrUpdate(product);

            foreach (ScaleCategory scaleCategory in ScaleCategorySeed.Seed())
                if (!context.Categories.Any(x => x.Title == scaleCategory.Title && x.Description == scaleCategory.Description))
                    context.Categories.AddOrUpdate(scaleCategory);

            context.SaveChanges();
        }
    }
}