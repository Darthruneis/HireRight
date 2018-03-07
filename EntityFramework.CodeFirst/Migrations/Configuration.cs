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
            AddBinderIfMissing(context, "Customer Care", Industry.CustomerServiceSales);
            AddBinderIfMissing(context, "Call Center (Sales) Achievement Drive", Industry.CustomerServiceSales);
            AddBinderIfMissing(context, "Call Center (Sales) Assertiveness", Industry.CustomerServiceSales);
            AddBinderIfMissing(context, "Call Center (Sales) Positive Attitude", Industry.CustomerServiceSales);
            AddBinderIfMissing(context, "Call Center (Sales) Reliability", Industry.CustomerServiceSales);
            AddBinderIfMissing(context, "Call Center (Sales) Self Confidence", Industry.CustomerServiceSales);
            AddBinderIfMissing(context, "Call Center (Sales) Service Ability", Industry.CustomerServiceSales);
            AddBinderIfMissing(context, "Call Center (Service) Customer Relations", Industry.CustomerServiceSales);
            AddBinderIfMissing(context, "Call Center (Service) Stress Management", Industry.CustomerServiceSales);
            AddBinderIfMissing(context, "Call Center (Service) Team Player", Industry.CustomerServiceSales);
            AddBinderIfMissing(context, "Call Center (Service) Willingness to Help", Industry.CustomerServiceSales);
            AddBinderIfMissing(context, "Service", Industry.CustomerServiceSales);
            AddBinderIfMissing(context, "SJT - Sales - Customer Focus", Industry.CustomerServiceSales);
            AddBinderIfMissing(context, "SJT - Sales - Drive and Persistence", Industry.CustomerServiceSales);
            AddBinderIfMissing(context, "SJT - Sales - Listening Skills", Industry.CustomerServiceSales);
            AddBinderIfMissing(context, "SJT - Sales - Sales Strategies", Industry.CustomerServiceSales);
            AddBinderIfMissing(context, "SJT Service: Conscientiousness", Industry.CustomerServiceSales);
            AddBinderIfMissing(context, "SJT Service: Interpersonal Skills", Industry.CustomerServiceSales);
            AddBinderIfMissing(context, "SJT Service: Listening Skills", Industry.CustomerServiceSales);
            AddBinderIfMissing(context, "SJT Service: Service-Orientation", Industry.CustomerServiceSales);

            AddBinderIfMissing(context, "SJT - Supervisor - Communication", Industry.Management);
            AddBinderIfMissing(context, "SJT - Supervisor - Conscientiousness", Industry.Management);
            AddBinderIfMissing(context, "SJT - Supervisor - Motivation", Industry.Management);
            AddBinderIfMissing(context, "SJT - Supervisor - Team Orientation", Industry.Management);

            AddBinderIfMissing(context, "SJT Management: Communication", Industry.Management);
            AddBinderIfMissing(context, "SJT Management: Decision Making", Industry.Management);
            AddBinderIfMissing(context, "SJT Management: Delegation", Industry.Management);
            AddBinderIfMissing(context, "SJT Management: Employee Relations", Industry.Management);

            AddBinderIfMissing(context, "C5 Business: Commitment", Industry.Management);
            AddBinderIfMissing(context, "C5 Business: Competitiveness", Industry.Management);
            AddBinderIfMissing(context, "C5 Business: Conscientiousness", Industry.Management);
            AddBinderIfMissing(context, "C5 Business: Control", Industry.Management);
            AddBinderIfMissing(context, "C5 Business: Cooperativeness", Industry.Management);

            AddBinderIfMissing(context, "Supervision", Industry.Management);

            AddBinderIfMissing(context, "Attention to Detail", Industry.General);
            AddBinderIfMissing(context, "Can-Do Attitude", Industry.General);
            AddBinderIfMissing(context, "Go-Getter Attitude", Industry.General);
            AddBinderIfMissing(context, "Assertiveness", Industry.General);
            AddBinderIfMissing(context, "Achievement Drive", Industry.General);
            AddBinderIfMissing(context, "Creativity", Industry.General);
            AddBinderIfMissing(context, "Drug Free Attitudes", Industry.General);
            AddBinderIfMissing(context, "Energy", Industry.General);
            AddBinderIfMissing(context, "Flexibility", Industry.General);
            AddBinderIfMissing(context, "Interpersonal Skills", Industry.General);
            AddBinderIfMissing(context, "Reliability", Industry.General);
            AddBinderIfMissing(context, "Responsibility", Industry.General);
            AddBinderIfMissing(context, "Safety", Industry.General);
            AddBinderIfMissing(context, "Self Confidence", Industry.General);
            AddBinderIfMissing(context, "Self Control", Industry.General);
            AddBinderIfMissing(context, "SJT Team: Confidence", Industry.General);
            AddBinderIfMissing(context, "SJT Team: Flexibility", Industry.General);
            AddBinderIfMissing(context, "SJT Team: Team Spirit", Industry.General);
            AddBinderIfMissing(context, "SJT Team: Trust", Industry.General);
            AddBinderIfMissing(context, "Social", Industry.General);
            AddBinderIfMissing(context, "Stress Management", Industry.General);
            AddBinderIfMissing(context, "Team Player", Industry.General);
            AddBinderIfMissing(context, "Trustworthiness", Industry.General);
            AddBinderIfMissing(context, "Work Ethic", Industry.General);
            AddBinderIfMissing(context, "Non-Violent Attitudes", Industry.General);

            AddBinderIfMissing(context, "Healthcare - Compassion", Industry.Medical);
            AddBinderIfMissing(context, "Healthcare - Patient Relations", Industry.Medical);
            AddBinderIfMissing(context, "Healthcare - Stress Tolerance", Industry.Medical);
            AddBinderIfMissing(context, "Healthcare - Team Player", Industry.Medical);
        }

        private void AddBinderIfMissing(HireRightDbContext context, string title, params long[] industryIds)
        {
            if (!industryIds.Any())
                return;

            var category = context.Categories.SingleOrDefault(x => x.Title == title);
            if (category == null)
                return;

            var existingBinders = context.IndustryScaleCategoryBinders.AsNoTracking().Where(x => x.CategoryId == category.Id).ToList();
            foreach (long id in industryIds)
            {
                if (existingBinders.All(x => x.IndustryId != id))
                {
                    var newBinder = new IndustryScaleCategory(id, category.Id);
                    category.IndustryBinders.Add(newBinder);
                    existingBinders.Add(newBinder);
                }
            }
        }
    }
}