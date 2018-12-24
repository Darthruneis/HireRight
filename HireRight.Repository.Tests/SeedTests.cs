using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web.Configuration;
using FluentAssertions;
using HireRight.Persistence.Database_Context;
using HireRight.Persistence.Models.CompanyAggregate;
using HireRight.Persistence.Seeds;
using NUnit.Framework;

namespace HireRight.Persistence.Tests
{
    public class SeedTests
    {
        [TestFixture]
        public class IndustryAssessmentBinderSeedTests
        {
            [Test]
            public void IndustryAssessmentBinderSeedDoesNotThrow()
            {
                IndustryAssessmentBinder[] seed;
                Action getSeed = () => seed = IndustryAssessmentBinderSeed.Seed(new HireRightDbContext());

                getSeed.ShouldNotThrow();
            }

            [Test]
            public void IndustryAssessmentBinderSeedCreatesActiveBinderObjects()
            {
                var seed = IndustryAssessmentBinderSeed.Seed(new HireRightDbContext());

                seed.Any(x => x.IsActive).Should().BeTrue();
            }
        }

        [TestFixture]
        public class IndustryScaleCategorySeedTests
        {
            [Test]
            public void IndustryScaleCategorySeedDoesNotThrow()
            {
                IndustryScaleCategory[] seed;
                Action getSeed = () => seed = IndustryScaleCategorySeed.SeedRelationships(new HireRightDbContext());

                getSeed.ShouldNotThrow();
            }

            [Test]
            public void IndustryScaleCategorySeedCreatesActiveBinderObjects()
            {
                var seed = IndustryScaleCategorySeed.SeedRelationships(new HireRightDbContext());

                seed.Any(x => x.IsActive).Should().BeTrue();
            }
        }

        [TestFixture]
        public class CategorySeedTests
        {
            [Test]
            public void DoesNotThrow()
            {
                Action action = () => ScaleCategorySeed.Seed();

                action.ShouldNotThrow();
            }
        }
    }
}