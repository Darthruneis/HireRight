using System;
using System.Collections.Generic;
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
            CodeGenerator = new BaseMigrationCodeGenerator();
        }

        protected override void Seed(HireRightDbContext context)
        {
            context.Products.AddOrUpdate(x => x.StaticId, ProductsSeed.Seed().ToArray());
            context.SaveChanges();

            List<ScaleCategory> scaleCategories = ScaleCategorySeed.Seed();
            context.Categories.AddOrUpdate(x => x.StaticId, scaleCategories.ToArray());
            context.Industries.AddOrUpdate(x => x.StaticId, IndustrySeed.Seed);
            context.Discounts.AddOrUpdate(x => x.StaticId, ProductsSeed.DiscountSeed().ToArray());
            context.SaveChanges();

            new IndustryScaleCategorySeed().SeedRelationships(context);
        }
    }
}