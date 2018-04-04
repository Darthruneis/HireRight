using System;
using System.Collections.Generic;
using HireRight.EntityFramework.CodeFirst.Database_Context;
using HireRight.EntityFramework.CodeFirst.Seeds;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Data.Entity;
using HireRight.EntityFramework.CodeFirst.Models.Abstract;
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
            context.AssessmentTypes.AddOrUpdate(x => x.StaticId, AssessmentTypeSeed.Seed);
            context.SaveChanges();

            List<ScaleCategory> scaleCategories = ScaleCategorySeed.Seed();
            context.Assessments.AddOrUpdate(x => x.Title, AssessmentSeed.Seed);
            context.Categories.AddOrUpdate(x => x.StaticId, scaleCategories.ToArray());
            context.Industries.AddOrUpdate(x => x.StaticId, IndustrySeed.Seed);
            context.Discounts.AddOrUpdate(x => x.StaticId, ProductsSeed.DiscountSeed().ToArray());
            context.SaveChanges();

            context.IndustryAssessmentBinders.AddOrUpdate(IndustryAssessmentBinderSeed.Seed(context));
            context.IndustryScaleCategoryBinders.AddOrUpdate(IndustryScaleCategorySeed.SeedRelationships(context));

            var industryScaleCategoryBindersToDeactivate =
                context.IndustryScaleCategoryBinders
                       .Include(x => x.Category)
                       .Include(x => x.Industry)
                       .Where(x => !x.Category.IsActive || !x.Industry.IsActive)
                       .ToList();

            industryScaleCategoryBindersToDeactivate.ForEach(x => x.IsActive = false);

            var assessmentScaleCategoryBindersToDeactivate =
                context.AssessmentScaleCategoryBinders
                       .Include(x => x.ScaleCategory)
                       .Include(x => x.Assessment)
                       .Where(x => !x.ScaleCategory.IsActive || !x.Assessment.IsActive)
                       .ToList();
            assessmentScaleCategoryBindersToDeactivate.ForEach(x => x.IsActive = false);

            var industryAssessmentBindersToDeactivate =
                context.IndustryAssessmentBinders
                       .Include(x => x.Industry)
                       .Include(x => x.Assessment)
                       .Where(x => !x.Industry.IsActive || !x.Assessment.IsActive)
                       .ToList();
            industryAssessmentBindersToDeactivate.ForEach(x => x.IsActive = false);

            context.SaveChanges();
        }
    }
}