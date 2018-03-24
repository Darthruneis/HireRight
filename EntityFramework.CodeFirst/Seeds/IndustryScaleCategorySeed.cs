﻿using System;
using System.Collections.Generic;
using System.Linq;
using HireRight.EntityFramework.CodeFirst.Database_Context;
using HireRight.EntityFramework.CodeFirst.Models.CompanyAggregate;

namespace HireRight.EntityFramework.CodeFirst.Seeds
{
    public class IndustryScaleCategorySeed
    {
        private readonly List<Exception> _errors = new List<Exception>();

        public void SeedRelationships(HireRightDbContext context)
        {
            context.Database.ExecuteSqlCommand("DELETE FROM dbo.IndustryScaleCategory");
            SetIndustryRelationshipsForCategories(context);
            if(_errors.Any())
                throw new AggregateException("Encountered erors with the configured binders. See the inner exceptions for details.", _errors);
            context.SaveChanges();

            BindUnboundCategoriesToOtherIndustry(context);
            if(_errors.Any())
                throw new AggregateException("Encountered erors with the automatic binders. See the inner exceptions for details.", _errors);
            context.SaveChanges();
        }

        private void BindUnboundCategoriesToOtherIndustry(HireRightDbContext context)
        {
            var categories = context.Categories.AsNoTracking().Where(x => !context.IndustryScaleCategoryBinders.Select(y => y.CategoryId).Contains(x.Id)).ToList();
            foreach (ScaleCategory scaleCategory in categories)
            {
                AddBinderIfMissing(context, scaleCategory.Title, Industry.Other);
            }
        }

        private void SetIndustryRelationshipsForCategories(HireRightDbContext context)
        {
            AddBinderIfMissing(context, "Achievement Drive", Industry.General, Industry.Management);
            AddBinderIfMissing(context, "Artistic", Industry.General);
            AddBinderIfMissing(context, "Assertiveness", Industry.General, Industry.Management);
            AddBinderIfMissing(context, "Attention to Detail", Industry.General);

            AddBinderIfMissing(context, "C5 Business: Commitment", Industry.Management);
            AddBinderIfMissing(context, "C5 Business: Competitiveness", Industry.Management);
            AddBinderIfMissing(context, "C5 Business: Conscientiousness", Industry.Management);
            AddBinderIfMissing(context, "C5 Business: Control", Industry.Management);
            AddBinderIfMissing(context, "C5 Business: Cooperativeness", Industry.Management);

            AddBinderIfMissing(context, "Call Center (Sales) Achievement Drive", Industry.Sales);
            AddBinderIfMissing(context, "Call Center (Sales) Assertiveness", Industry.Sales);
            AddBinderIfMissing(context, "Call Center (Sales) Positive Attitude", Industry.Sales);
            AddBinderIfMissing(context, "Call Center (Sales) Reliability", Industry.Sales);
            AddBinderIfMissing(context, "Call Center (Sales) Self Confidence", Industry.Sales);
            AddBinderIfMissing(context, "Call Center (Sales) Service Ability", Industry.Sales);

            AddBinderIfMissing(context, "Call Center (Service) Customer Relations", Industry.CustomerService);
            AddBinderIfMissing(context, "Call Center (Service) Stress Management", Industry.CustomerService);
            AddBinderIfMissing(context, "Call Center (Service) Team Player", Industry.CustomerService);
            AddBinderIfMissing(context, "Call Center (Service) Willingness To Help", Industry.CustomerService);

            AddBinderIfMissing(context, "Can-Do Attitude", Industry.General);
            AddBinderIfMissing(context, "Candidness", Industry.General);
            AddBinderIfMissing(context, "Conventional", Industry.Office);
            AddBinderIfMissing(context, "Creativity", Industry.General);
            AddBinderIfMissing(context, "Customer Care", Industry.Pharmaceutical, Industry.CustomerService, Industry.Office);

            AddBinderIfMissing(context, "Drug Free Attitudes", Industry.General);

            AddBinderIfMissing(context, "Energy", Industry.General);
            AddBinderIfMissing(context, "Enterprising", Industry.Management, Industry.Sales);
            AddBinderIfMissing(context, "Extraversion", Industry.General);

            AddBinderIfMissing(context, "Flexibility", Industry.General, Industry.Management);

            AddBinderIfMissing(context, "Good Citizen", Industry.General);

            AddBinderIfMissing(context, "Healthcare - Compassion", Industry.HealthCare);
            AddBinderIfMissing(context, "Healthcare - Patient Relations", Industry.HealthCare);
            AddBinderIfMissing(context, "Healthcare - Stress Tolerance", Industry.HealthCare);
            AddBinderIfMissing(context, "Healthcare - Team Player", Industry.HealthCare);
            AddBinderIfMissing(context, "Helping Disposition", Industry.General, Industry.Management);

            AddBinderIfMissing(context, "Inspection", Industry.Manufacturing);
            AddBinderIfMissing(context, "Interpersonal Skills", Industry.General);
            AddBinderIfMissing(context, "Investigative", Industry.General);

            AddBinderIfMissing(context, "Kindness", Industry.General);

            AddBinderIfMissing(context, "Language Skills", Industry.General);
            AddBinderIfMissing(context, "Leadership", Industry.Management);
            AddBinderIfMissing(context, "Light Industrial Math", Industry.Manufacturing);

            AddBinderIfMissing(context, "Math Skills", Industry.General);
            AddBinderIfMissing(context, "Mathematical and Logical Reasoning", Industry.General);

            AddBinderIfMissing(context, "Non-Violent Attitudes", Industry.General);

            AddBinderIfMissing(context, "OCEAN - Agreeableness", Industry.General);
            AddBinderIfMissing(context, "OCEAN - Conscientiousness", Industry.General);
            AddBinderIfMissing(context, "OCEAN - Extraversion", Industry.General);
            AddBinderIfMissing(context, "OCEAN - Non-Negativity", Industry.General);
            AddBinderIfMissing(context, "OCEAN - Openness", Industry.General);

            AddBinderIfMissing(context, "PAP: Dedication", Industry.General);
            AddBinderIfMissing(context, "PAP: Initiative", Industry.General);
            AddBinderIfMissing(context, "PAP: Open Mindedness", Industry.General);
            AddBinderIfMissing(context, "PAP: Optimism", Industry.General);
            AddBinderIfMissing(context, "Problem Solving Interest", Industry.General);

            AddBinderIfMissing(context, "Realistic", Industry.General);
            AddBinderIfMissing(context, "Reasoning", Industry.General);
            AddBinderIfMissing(context, "Reliability", Industry.General);
            AddBinderIfMissing(context, "Responsibility", Industry.General);
            AddBinderIfMissing(context, "Rules Compliance", Industry.General);

            AddBinderIfMissing(context, "Safety", Industry.General);
            AddBinderIfMissing(context, "Self Confidence", Industry.General);
            AddBinderIfMissing(context, "Self Control", Industry.General);
            AddBinderIfMissing(context, "Service", Industry.General);
            AddBinderIfMissing(context, "SJT - Sales - Customer Focus", Industry.Sales);
            AddBinderIfMissing(context, "SJT - Sales - Drive and Persistence", Industry.Sales);
            AddBinderIfMissing(context, "SJT - Sales - Listening Skills", Industry.Sales);
            AddBinderIfMissing(context, "SJT - Sales - Sales Strategies", Industry.Sales);
            AddBinderIfMissing(context, "SJT - Supervisor - Communication", Industry.Management);
            AddBinderIfMissing(context, "SJT - Supervisor - Conscientiousness", Industry.Management);
            AddBinderIfMissing(context, "SJT - Supervisor - Motivation", Industry.Management);
            AddBinderIfMissing(context, "SJT - Supervisor - Team Orientation", Industry.Management);
            AddBinderIfMissing(context, "SJT - Management - Communication", Industry.Management);
            AddBinderIfMissing(context, "SJT - Management - Decision Making", Industry.Management);
            AddBinderIfMissing(context, "SJT - Management - Delegation", Industry.Management);
            AddBinderIfMissing(context, "SJT - Management - Employee Relations", Industry.Management);
            AddBinderIfMissing(context, "SJT - Service - Conscientiousness", Industry.CustomerService);
            AddBinderIfMissing(context, "SJT - Service - Interpersonal Skills", Industry.CustomerService);
            AddBinderIfMissing(context, "SJT - Service - Listening Skills", Industry.CustomerService);
            AddBinderIfMissing(context, "SJT - Service - Service-Orientation", Industry.CustomerService);
            AddBinderIfMissing(context, "SJT - Team - Confidence", Industry.General);
            AddBinderIfMissing(context, "SJT - Team - Flexibility", Industry.General);
            AddBinderIfMissing(context, "SJT - Team - Team Spirit", Industry.General);
            AddBinderIfMissing(context, "SJT - Team - Trust", Industry.General);
            AddBinderIfMissing(context, "Social", Industry.General);
            AddBinderIfMissing(context, "Spatial Reasoning", Industry.Manufacturing);
            AddBinderIfMissing(context, "Stress Management", Industry.General, Industry.Management);
            AddBinderIfMissing(context, "Supervision", Industry.Management);

            AddBinderIfMissing(context, "Tables", Industry.General);
            AddBinderIfMissing(context, "Team Care", Industry.General);
            AddBinderIfMissing(context, "Team Player", Industry.General);
            AddBinderIfMissing(context, "Trustworthiness", Industry.General, Industry.Management);

            AddBinderIfMissing(context, "Verbal Reasoning", Industry.General);

            AddBinderIfMissing(context, "Work Ethic", Industry.General);
        }

        private void AddBinderIfMissing(HireRightDbContext context, string title, params long[] industryIds)
        {
            if (!industryIds.Any())
                return;

            try
            {
                var category = context.Categories.AsNoTracking().Where(x => x.Title == title).Select(x => x.Id).Single();

                AddBinderIfMissing(context, category, industryIds);
            }
            catch (Exception ex)
            {
                var wrappedEx = new Exception("Unable to add binder between category \'" + title + "\' and industries: " + string.Join(",", industryIds) + ".", ex);
                _errors.Add(wrappedEx);
            }
        }

        private static void AddBinderIfMissing(HireRightDbContext context, long categoryId, params long[] industryIds)
        {
            var existingBinders = context.IndustryScaleCategoryBinders.AsNoTracking().Where(x => x.CategoryId == categoryId).ToList();
            var industries = context.Industries.AsNoTracking().ToList();
            foreach (long id in industryIds)
            {
                var industry = industries.Single(x => x.StaticId == id);
                if (existingBinders.All(x => x.IndustryId != industry.Id))
                {
                    var newBinder = new IndustryScaleCategory(industry.Id, categoryId);
                    context.IndustryScaleCategoryBinders.Add(newBinder);
                    existingBinders.Add(newBinder);
                }
            }
        }
    }
}