using System.Collections.Generic;
using HireRight.Persistence.Models.CompanyAggregate;

namespace HireRight.Persistence.Seeds
{
    public static class AssessmentSeed
    {
        public static Assessment[] Seed = GetSeed().ToArray();

        private static List<Assessment> GetSeed()
        {
            var seed = new List<Assessment>();
            //General
            seed.Add(new Assessment("Elite Character Profile", AssessmentType.Profile, 18));
            seed.Add(new Assessment("Elite Skills Profile", AssessmentType.Profile, 35));
            seed.Add(new Assessment("Elite Staffing Profile", AssessmentType.Profile, 15));
            seed.Add(new Assessment("Positive Attitude Profile", AssessmentType.Profile, 12));
            seed.Add(new Assessment("Quick Screen", AssessmentType.Test, 10, true));

            //Customer Service & Sales
            seed.Add(new Assessment("Elite Call Center Service Profile", AssessmentType.Profile, 12));
            seed.Add(new Assessment("Elite Call Center Sales Profile", AssessmentType.Profile, 15));
            seed.Add(new Assessment("Elite Sales Profile", AssessmentType.Profile, 15));
            seed.Add(new Assessment("Custom Sales Profile", AssessmentType.Profile));
            seed.Add(new Assessment("Elite Hospitality Profile", AssessmentType.Profile, 30));
            seed.Add(new Assessment("Elite Service Profile", AssessmentType.Profile, 12));
            seed.Add(new Assessment("Situational Judgment - Service", AssessmentType.Test, 15));
            seed.Add(new Assessment("Situational Judgment - Team Player", AssessmentType.Test, 15));
            seed.Add(new Assessment("Situational Judgment - Sales", AssessmentType.Test, 15));

            //Healthcare
            seed.Add(new Assessment("Elite Healthcare Profile", AssessmentType.Profile, 12));
            seed.Add(new Assessment("Elite Care Profile", AssessmentType.Profile, 25));
            seed.Add(new Assessment("Pharmacist Profile", AssessmentType.Profile));
            seed.Add(new Assessment("Pharmacy Technician Profile", AssessmentType.Profile, 30));
            seed.Add(new Assessment("Dental Hygienist Custom Profile", AssessmentType.Profile));
            seed.Add(new Assessment("Dental Office General Custom Profile", AssessmentType.Profile));

            //Management & Professional
            seed.Add(new Assessment("Elite Manager Profile", AssessmentType.Profile, 21));
            seed.Add(new Assessment("Intellect Profile", AssessmentType.Profile, 12));
            seed.Add(new Assessment("Elite Supervisor Profile", AssessmentType.Profile, 18));
            seed.Add(new Assessment("Elite Personality Profile", AssessmentType.Profile, 32));
            seed.Add(new Assessment("Innovation Profile", AssessmentType.Profile, 15));
            seed.Add(new Assessment("Integrity Profile", AssessmentType.Profile));
            seed.Add(new Assessment("Emotional Intelligence", AssessmentType.Test));
            seed.Add(new Assessment("Situational Judgment - Supervisor", AssessmentType.Test, 15));
            seed.Add(new Assessment("Situational Judgment - Manager", AssessmentType.Test, 15));
            seed.Add(new Assessment("Workplace Aptitude", AssessmentType.Test, 12));
            seed.Add(new Assessment("Grit Profile", AssessmentType.Profile));
            seed.Add(new Assessment("Elite Business Profile", AssessmentType.Profile));
            seed.Add(new Assessment("Custom Executive Profile", AssessmentType.Profile, 35));
            //seed.Add(new Assessment("Custom Profile or Values", AssessmentType.Profile));

            //Manufacturing
            seed.Add(new Assessment("Elite Industrial Profile", AssessmentType.Profile, 34));
            seed.Add(new Assessment("Safety Profile", AssessmentType.Profile));
            seed.Add(new Assessment("Mechanical Ability Profile", AssessmentType.Profile, 15, true));
            
            //Other
            //Transportation
            seed.Add(new Assessment("Elite Transportation Profile", AssessmentType.Profile, 18));

            //Retail & Banking
            seed.Add(new Assessment("Elite Retail Profile", AssessmentType.Profile, 33));
            seed.Add(new Assessment("Elite Banking Profile", AssessmentType.Profile, 33));


            return seed;
        }
    }
}