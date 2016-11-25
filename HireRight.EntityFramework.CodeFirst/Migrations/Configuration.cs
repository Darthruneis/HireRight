using HireRight.EntityFramework.CodeFirst.Database_Context;

namespace HireRight.EntityFramework.CodeFirst.Migrations
{
    using Models;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<HireRightDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            CodeGenerator = new BaseMigrationCodeGenerator();
        }

        protected override void Seed(HireRightDbContext context)
        {
            List<Discount> discounts = new List<Discount>
            {
                new Discount(true, 0.20m, 100),
                new Discount(false, 8.00m, 500),
                new Discount(true, 0.30m, 1000),
                new Discount(false, 10.00m, 10000)
            };

            Product product = new Product("Assessment Test", 25.00m, discounts);

            context.Products.AddOrUpdate(product);
            context.SaveChanges();

            // This method will be called after migrating to the latest version.

            // You can use the DbSet<T>.AddOrUpdate() helper extension method to avoid creating duplicate seed data. E.g.
            //
            // context.People.AddOrUpdate( p => p.FullName, new Person { FullName = "Andrew Peters" }, new Person { FullName = "Brice Lambson" }, new Person {
            // FullName = "Rowan Miller" } );
        }
    }
}