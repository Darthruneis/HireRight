using HireRight.EntityFramework.CodeFirst.Seeds;
using NUnit.Framework;

namespace HireRight.Repository.Tests
{
    [TestFixture]
    public class CategorySeedTests
    {
        [Test]
        public void DoesNotThrow()
        {
            var result = ScaleCategorySeed.Seed();
        }

        [Test]
        public void ToJsonStringTesting()
        {
            ScaleCategorySeed.UpdateJsonFile(ScaleCategorySeed.Seed());
        }
    }
}