using HireRight.EntityFramework.CodeFirst.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace HireRight.EntityFramework.CodeFirst.Database_Context
{
    public class HireRightDbContextInitializer : CreateDatabaseIfNotExists<HireRightDbContext>
    {
        /// <summary>
        /// A method that should be overridden to actually add data to the context for seeding. The default implementation does nothing.
        /// </summary>
        /// <param name="context">The context to seed.</param>
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

            base.Seed(context);
        }
    }
}