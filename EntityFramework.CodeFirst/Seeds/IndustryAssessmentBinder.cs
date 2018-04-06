using System.Collections.Generic;
using System.Linq;
using HireRight.Persistence.Database_Context;
using HireRight.Persistence.Models.CompanyAggregate;

namespace HireRight.Persistence.Seeds
{
    public static class IndustryAssessmentBinderSeed
    {
        public static IndustryAssessmentBinder[] Seed(HireRightDbContext context)
        {
            var assessments = context.Assessments.ToList();
            var seed = new List<IndustryAssessmentBinder>();
            seed.AddBinderIfAssessmentExists(assessments.SingleOrDefault(x => x.Title.Equals("Quick Screen")), Industry.CustomerService, Industry.Sales, Industry.Other, Industry.Manufacturing);
            seed.AddBinderIfAssessmentExists(assessments.SingleOrDefault(x => x.Title.Equals("Elite Character Profile")), Industry.General, Industry.HealthCare, Industry.Other, Industry.Office, Industry.Manufacturing);
            seed.AddBinderIfAssessmentExists(assessments.SingleOrDefault(x => x.Title.Equals("Elite Staffing Profile")), Industry.General, Industry.HealthCare, Industry.Manufacturing);
            seed.AddBinderIfAssessmentExists(assessments.SingleOrDefault(x => x.Title.Equals("Positive Attitude Profile")), Industry.General, Industry.Management, Industry.Office, Industry.Other);
            seed.AddBinderIfAssessmentExists(assessments.SingleOrDefault(x => x.Title.Equals("Elite Skills Profile")), Industry.General, Industry.Management, Industry.HealthCare, Industry.Office, Industry.Other);

            seed.AddBinderIfAssessmentExists(assessments.SingleOrDefault(x => x.Title.Equals("Elite Call Center Service Profile")), Industry.CustomerService);
            seed.AddBinderIfAssessmentExists(assessments.SingleOrDefault(x => x.Title.Equals("Elite Call Center Sales Profile")), Industry.Sales);
            seed.AddBinderIfAssessmentExists(assessments.SingleOrDefault(x => x.Title.Equals("Elite Sales Profile")), Industry.Sales);
            seed.AddBinderIfAssessmentExists(assessments.SingleOrDefault(x => x.Title.Equals("Elite Service Profile")), Industry.CustomerService);
            seed.AddBinderIfAssessmentExists(assessments.SingleOrDefault(x => x.Title.Equals("Custom Sales Profile")), Industry.Sales);
            seed.AddBinderIfAssessmentExists(assessments.SingleOrDefault(x => x.Title.Equals("Elite Hospitality Profile")), Industry.CustomerService, Industry.HealthCare);
            seed.AddBinderIfAssessmentExists(assessments.SingleOrDefault(x => x.Title.Equals("Situational Judgment - Service")), Industry.CustomerService, Industry.Other, Industry.Office);
            seed.AddBinderIfAssessmentExists(assessments.SingleOrDefault(x => x.Title.Equals("Situational Judgment - Team Player")), Industry.CustomerService, Industry.Office, Industry.Sales, Industry.Other);
            seed.AddBinderIfAssessmentExists(assessments.SingleOrDefault(x => x.Title.Equals("Situational Judgment - Sales")), Industry.Sales, Industry.Other);

            seed.AddBinderIfAssessmentExists(assessments.SingleOrDefault(x => x.Title.Equals("Elite Healthcare Profile")), Industry.HealthCare);
            seed.AddBinderIfAssessmentExists(assessments.SingleOrDefault(x => x.Title.Equals("Elite Care Profile")), Industry.HealthCare);
            seed.AddBinderIfAssessmentExists(assessments.SingleOrDefault(x => x.Title.Equals("Pharmacist Profile")), Industry.HealthCare);
            seed.AddBinderIfAssessmentExists(assessments.SingleOrDefault(x => x.Title.Equals("Pharmacy Technician Profile")), Industry.HealthCare);
            seed.AddBinderIfAssessmentExists(assessments.SingleOrDefault(x => x.Title.Equals("Dental Hygienist Custom Profile")), Industry.HealthCare);
            seed.AddBinderIfAssessmentExists(assessments.SingleOrDefault(x => x.Title.Equals("Dental Office General Custom Profile")), Industry.HealthCare);

            seed.AddBinderIfAssessmentExists(assessments.SingleOrDefault(x => x.Title.Equals("Elite Manager Profile")), Industry.Management);
            seed.AddBinderIfAssessmentExists(assessments.SingleOrDefault(x => x.Title.Equals("Intellect Profile")), Industry.Management);
            seed.AddBinderIfAssessmentExists(assessments.SingleOrDefault(x => x.Title.Equals("Elite Supervisor Profile")), Industry.Management);
            seed.AddBinderIfAssessmentExists(assessments.SingleOrDefault(x => x.Title.Equals("Elite Personality Profile")), Industry.Management, Industry.Office);
            seed.AddBinderIfAssessmentExists(assessments.SingleOrDefault(x => x.Title.Equals("Innovation Profile")), Industry.Management);
            seed.AddBinderIfAssessmentExists(assessments.SingleOrDefault(x => x.Title.Equals("Integrity Profile")), Industry.Management);
            seed.AddBinderIfAssessmentExists(assessments.SingleOrDefault(x => x.Title.Equals("Emotional Intelligence")), Industry.Management);
            seed.AddBinderIfAssessmentExists(assessments.SingleOrDefault(x => x.Title.Equals("Situational Judgment - Supervisor")), Industry.Management);
            seed.AddBinderIfAssessmentExists(assessments.SingleOrDefault(x => x.Title.Equals("Situational Judgment - Manager")), Industry.Management);
            seed.AddBinderIfAssessmentExists(assessments.SingleOrDefault(x => x.Title.Equals("Workplace Aptitude")), Industry.Management, Industry.Office, Industry.Other);
            seed.AddBinderIfAssessmentExists(assessments.SingleOrDefault(x => x.Title.Equals("Grit Profile")), Industry.Management);
            seed.AddBinderIfAssessmentExists(assessments.SingleOrDefault(x => x.Title.Equals("Elite Business Profile")), Industry.Management);
            seed.AddBinderIfAssessmentExists(assessments.SingleOrDefault(x => x.Title.Equals("Custom Executive Profile")), Industry.Management);

            seed.AddBinderIfAssessmentExists(assessments.SingleOrDefault(x => x.Title.Equals("Elite Industrial Profile")), Industry.Manufacturing);
            seed.AddBinderIfAssessmentExists(assessments.SingleOrDefault(x => x.Title.Equals("Safety Profile")), Industry.Manufacturing);
            seed.AddBinderIfAssessmentExists(assessments.SingleOrDefault(x => x.Title.Equals("Mechanical Ability Profile")), Industry.Manufacturing);

            seed.AddBinderIfAssessmentExists(assessments.SingleOrDefault(x => x.Title.Equals("Elite Transportation Profile")), Industry.Other);
            seed.AddBinderIfAssessmentExists(assessments.SingleOrDefault(x => x.Title.Equals("Elite Retail Profile")), Industry.Other);
            seed.AddBinderIfAssessmentExists(assessments.SingleOrDefault(x => x.Title.Equals("Elite Banking Profile")), Industry.Other);

            return seed.ToArray();
        }

        private static void AddBinderIfAssessmentExists(this List<IndustryAssessmentBinder> seed, Assessment assessment, params long[] industryIds)
        {
            if (assessment == null)
                return;

            seed.AddRange(industryIds.Select(industryId => new IndustryAssessmentBinder(assessment.Id, industryId)));
        }
    }
}