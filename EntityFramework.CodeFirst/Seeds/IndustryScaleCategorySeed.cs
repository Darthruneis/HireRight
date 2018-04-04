using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using HireRight.EntityFramework.CodeFirst.Database_Context;
using HireRight.EntityFramework.CodeFirst.Models.CompanyAggregate;

namespace HireRight.EntityFramework.CodeFirst.Seeds
{
    public static class IndustryScaleCategorySeed
    {
        private static readonly List<ScaleCategory> CategoriesFromSeed = ScaleCategorySeed.Seed();

        public static IndustryScaleCategory[] SeedRelationships(HireRightDbContext context)
        {
            List<IndustryScaleCategory> seedData = CreateRelationshipsUsingStaticIds();
            seedData.BindUnboundCategoriesToOtherIndustryUsingStaticIds(context);

            return seedData.Where(x => context.IndustryScaleCategoryBinders.All(y => y.CategoryId != x.CategoryId && y.IndustryId != x.IndustryId)).ToArray();
        }

        private static void BindUnboundCategoriesToOtherIndustryUsingStaticIds(this List<IndustryScaleCategory> seedData, HireRightDbContext context)
        {
            var categories = context.Categories.AsNoTracking().ToList();
            foreach (ScaleCategory scaleCategory in categories.Where(x => seedData.All(y => y.CategoryId != x.Id)))
            {
                seedData.AddRange(CreateBindersFromStaticIds(scaleCategory.Title, Industry.Other));
            }
        }

        public static List<IndustryScaleCategory> CreateRelationshipsUsingStaticIds()
        {
            var bindersWithStaticIds = new List<IndustryScaleCategory>();

            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("Achievement Drive", Industry.General, Industry.Management));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("Artistic", Industry.General));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("Assertiveness", Industry.General, Industry.Management));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("Attention to Detail", Industry.General));

            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("C5 Business: Commitment", Industry.Management));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("C5 Business: Competitiveness", Industry.Management));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("C5 Business: Conscientiousness", Industry.Management));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("C5 Business: Control", Industry.Management));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("C5 Business: Cooperativeness", Industry.Management));

            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("Call Center (Sales) Achievement Drive", Industry.Sales));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("Call Center (Sales) Assertiveness", Industry.Sales));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("Call Center (Sales) Positive Attitude", Industry.Sales));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("Call Center (Sales) Reliability", Industry.Sales));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("Call Center (Sales) Self Confidence", Industry.Sales));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("Call Center (Sales) Service Ability", Industry.Sales));

            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("Call Center (Service) Customer Relations", Industry.CustomerService));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("Call Center (Service) Stress Management", Industry.CustomerService));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("Call Center (Service) Team Player", Industry.CustomerService));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("Call Center (Service) Willingness to Help", Industry.CustomerService));

            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("Can-Do Attitude", Industry.General));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("Candidness", Industry.General));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("Conventional", Industry.Office));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("Creativity", Industry.General));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("Customer Care", Industry.Pharmaceutical, Industry.CustomerService, Industry.Office));

            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("Drug Free Attitudes", Industry.General));

            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("Energy", Industry.General));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("Enterprising", Industry.Management, Industry.Sales));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("Extraversion", Industry.General));

            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("Flexibility", Industry.General, Industry.Management));

            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("Good Citizen", Industry.General));

            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("Healthcare - Compassion", Industry.HealthCare));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("Healthcare - Patient Relations", Industry.HealthCare));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("Healthcare - Stress Tolerance", Industry.HealthCare));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("Healthcare - Team Player", Industry.HealthCare));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("Helping Disposition", Industry.General, Industry.Management));

            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("Inspection", Industry.Manufacturing));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("Interpersonal Skills", Industry.General));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("Investigative", Industry.General));

            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("Kindness", Industry.General));

            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("Language Skills", Industry.General));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("Leadership", Industry.Management));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("Light Industrial Math", Industry.Manufacturing));

            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("Math Skills", Industry.General));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("Mathematical and Logical Reasoning", Industry.General));

            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("Non-Violent Attitudes", Industry.General));

            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("OCEAN - Agreeableness", Industry.General));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("OCEAN - Conscientiousness", Industry.General));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("OCEAN - Extraversion", Industry.General));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("OCEAN - Non-Negativity", Industry.General));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("OCEAN - Openness", Industry.General));

            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("PAP: Dedication", Industry.General));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("PAP: Initiative", Industry.General));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("PAP: Open Mindedness", Industry.General));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("PAP: Optimism", Industry.General));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("Problem Solving Interest", Industry.General));

            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("Realistic", Industry.General));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("Reasoning", Industry.General));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("Reliability", Industry.General));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("Responsibility", Industry.General));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("Rules Compliance", Industry.General));

            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("Safety", Industry.General));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("Self Confidence", Industry.General));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("Self Control", Industry.General));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("Service", Industry.General));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("SJT - Sales - Customer Focus", Industry.Sales));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("SJT - Sales - Drive and Persistence", Industry.Sales));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("SJT - Sales - Listening Skills", Industry.Sales));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("SJT - Sales - Sales Strategies", Industry.Sales));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("SJT - Supervisor - Communication", Industry.Management));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("SJT - Supervisor - Conscientiousness", Industry.Management));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("SJT - Supervisor - Motivation", Industry.Management));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("SJT - Supervisor - Team Orientation", Industry.Management));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("SJT - Management - Communication", Industry.Management));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("SJT - Management - Decision Making", Industry.Management));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("SJT - Management - Delegation", Industry.Management));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("SJT - Management - Employee Relations", Industry.Management));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("SJT - Service - Conscientiousness", Industry.CustomerService));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("SJT - Service - Interpersonal Skills", Industry.CustomerService));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("SJT - Service - Listening Skills", Industry.CustomerService));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("SJT - Service - Service-Orientation", Industry.CustomerService));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("SJT - Team - Confidence", Industry.General));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("SJT - Team - Flexibility", Industry.General));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("SJT - Team - Team Spirit", Industry.General));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("SJT - Team - Trust", Industry.General));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("Social", Industry.General));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("Spatial Reasoning", Industry.Manufacturing));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("Stress Management", Industry.General, Industry.Management));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("Supervision", Industry.Management));

            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("Tables", Industry.General));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("Team Care", Industry.General));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("Team Player", Industry.General));
            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("Trustworthiness", Industry.General, Industry.Management));

            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("Verbal Reasoning", Industry.General));

            bindersWithStaticIds.AddRange(CreateBindersFromStaticIds("Work Ethic", Industry.General));

            return bindersWithStaticIds;
        }

        private static List<IndustryScaleCategory> CreateBindersFromStaticIds(string title, params long[] industryIds)
        {
            var binders = new List<IndustryScaleCategory>();
            if (!industryIds.Any())
                return binders;

            var category = CategoriesFromSeed.Single(x => x.Title.ToLower() == title.ToLower());
            foreach (long industryId in industryIds)
            {
                binders.Add(new IndustryScaleCategory(industryId, category.StaticId));
            }
            return binders;
        }
    }
}