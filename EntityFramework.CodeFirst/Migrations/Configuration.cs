using System;
using HireRight.EntityFramework.CodeFirst.Database_Context;
using HireRight.EntityFramework.CodeFirst.Seeds;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Data.Entity;
using System.Linq.Expressions;
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
            foreach (Product product in ProductsSeed.Seed())
                if (!context.Products.Any(x => x.Title == product.Title && x.Price == product.Price && x.Discounts.Count == product.Discounts.Count))
                    context.Products.AddOrUpdate(product);

            foreach (ScaleCategory scaleCategory in ScaleCategorySeed.Seed())
                if (!context.Categories.Any(x => x.Title == scaleCategory.Title && x.Description == scaleCategory.Description))
                    context.Categories.AddOrUpdate(scaleCategory);

            context.Industries.AddOrUpdate(x => x.Id, IndustrySeed.Seed);
            context.SaveChanges();

            SetIndustryRelationshipsForCategories(context);
            context.SaveChanges();
        }

        private void SetIndustryRelationshipsForCategories(HireRightDbContext context)
        {
            var customerService = context.Categories.Single(x => x.Title == "Customer Care");
            if (!context.IndustryScaleCategoryBinders.Any(x => x.CategoryId == customerService.Id && x.IndustryId == Industry.CustomerServiceSales))
                customerService.IndustryBinders.Add(new IndustryScaleCategory(Industry.CustomerServiceSales, customerService.Id));

            //TODO: Map the rest of these relationships...
        }
    }
}