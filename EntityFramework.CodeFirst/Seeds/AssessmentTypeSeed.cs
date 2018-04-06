using System.Collections.Generic;
using HireRight.Persistence.Models.CompanyAggregate;

namespace HireRight.Persistence.Seeds
{
    public static class AssessmentTypeSeed
    {
        public static AssessmentType[] Seed = GetSeed().ToArray();

        private static List<AssessmentType> GetSeed()
        {
            var seed = new List<AssessmentType>();
            seed.Add(new AssessmentType("Test", AssessmentType.Test));
            seed.Add(new AssessmentType("Profile", AssessmentType.Profile));
            return seed;
        }
    }
}