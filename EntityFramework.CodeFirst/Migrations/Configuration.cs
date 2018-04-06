using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using HireRight.Persistence.Database_Context;
using HireRight.Persistence.Models.CompanyAggregate;
using HireRight.Persistence.Seeds;

namespace HireRight.Persistence.Migrations
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
            //ensure that any items not in the seed are deactivated
            context.Database.ExecuteSqlCommand("UPDATE dbo.Discount SET IsActive = 0");
            context.Database.ExecuteSqlCommand("UPDATE dbo.IndustryAssessmentBinder SET IsActive = 0");
            context.Database.ExecuteSqlCommand("UPDATE dbo.IndustryScaleCategory SET IsActive = 0");
            context.Database.ExecuteSqlCommand("UPDATE dbo.AssessmentScaleCategoryBinder SET IsActive = 0");

            context.Products.AddOrUpdate(x => x.StaticId, ProductsSeed.Seed().ToArray());
            context.AssessmentTypes.AddOrUpdate(x => x.StaticId, AssessmentTypeSeed.Seed);
            context.SaveChanges();

            List<ScaleCategory> scaleCategories = ScaleCategorySeed.Seed();
            context.Assessments.AddOrUpdate(x => x.Title, AssessmentSeed.Seed);
            context.Categories.AddOrUpdate(x => x.StaticId, scaleCategories.ToArray());
            context.Industries.AddOrUpdate(x => x.StaticId, IndustrySeed.Seed);
            context.Discounts.AddOrUpdate(x => x.StaticId, ProductsSeed.DiscountSeed().ToArray());
            context.SaveChanges();

            var industryAssessmentBinderSeed = IndustryAssessmentBinderSeed.Seed(context);
            var existingIndustryAssessmentBinders = context.IndustryAssessmentBinders.ToList();
            for (var i = 0; i < industryAssessmentBinderSeed.Length; i++)
            {
                IndustryAssessmentBinder binder = industryAssessmentBinderSeed[i];
                var existingBinder = existingIndustryAssessmentBinders.FirstOrDefault(x => x.IndustryId == binder.IndustryId && x.AssessmentId == binder.AssessmentId);
                if (existingBinder != null)
                {
                    existingBinder.IsActive = true;
                    industryAssessmentBinderSeed[i] = existingBinder;
                }
            }
            context.IndustryAssessmentBinders.AddOrUpdate(x => x.Id, industryAssessmentBinderSeed.ToArray());

            var industryScaleCategorySeed = IndustryScaleCategorySeed.SeedRelationships(context);
            var existingIndustryScaleCategories = context.IndustryScaleCategoryBinders.ToList();
            for (var i = 0; i < industryScaleCategorySeed.Length; i++)
            {
                IndustryScaleCategory binder = industryScaleCategorySeed[i];
                var existingBinder = existingIndustryScaleCategories.FirstOrDefault(x => x.IndustryId == binder.IndustryId && x.CategoryId == binder.CategoryId);
                if (existingBinder != null)
                {
                    existingBinder.IsActive = true;
                    industryScaleCategorySeed[i] = existingBinder;
                }
            }

            context.IndustryScaleCategoryBinders.AddOrUpdate(x => x.Id, industryScaleCategorySeed.ToArray());
            context.SaveChanges();

            var industryScaleCategoryBindersToDeactivate =
                context.IndustryScaleCategoryBinders
                       .Where(x => x.IsActive)
                       .Include(x => x.Category)
                       .Include(x => x.Industry)
                       .Where(x => !x.Category.IsActive || !x.Industry.IsActive)
                       .ToList();

            industryScaleCategoryBindersToDeactivate.ForEach(x => x.IsActive = false);

            var assessmentScaleCategoryBindersToDeactivate =
                context.AssessmentScaleCategoryBinders
                       .Where(x => x.IsActive)
                       .Include(x => x.ScaleCategory)
                       .Include(x => x.Assessment)
                       .Where(x => !x.ScaleCategory.IsActive || !x.Assessment.IsActive)
                       .ToList();
            assessmentScaleCategoryBindersToDeactivate.ForEach(x => x.IsActive = false);

            var industryAssessmentBindersToDeactivate =
                context.IndustryAssessmentBinders
                       .Where(x => x.IsActive)
                       .Include(x => x.Industry)
                       .Include(x => x.Assessment)
                       .Where(x => !x.Industry.IsActive || !x.Assessment.IsActive)
                       .ToList();
            industryAssessmentBindersToDeactivate.ForEach(x => x.IsActive = false);

            context.SaveChanges();
        }
    }
}