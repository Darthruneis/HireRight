using System;
using System.Linq;
using FluentAssertions;
using HireRight.EntityFramework.CodeFirst.Database_Context;
using HireRight.EntityFramework.CodeFirst.Models.CompanyAggregate;
using HireRight.EntityFramework.CodeFirst.Seeds;
using NUnit.Framework;

namespace HireRight.Repository.Tests
{
    [TestFixture]
    public class SeedTests
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
}