using System;
using DataTransferObjects.Filters.Concrete;
using FluentAssertions;
using NUnit.Framework;

namespace DataTransferTests
{
    [Category("Category Filter")]
    [TestFixture]
    public class CategoryFilterTests
    {
        [Test]
        public void CreateQueryAllQuerySet()
        {
            //arrange
            CategoryFilter filter = new CategoryFilter(1, 10, Guid.NewGuid());
            filter.TitleFilter = "title";
            filter.DescriptionFilter = "description";

            //act
            string queryNoBase = filter.CreateQuery(false);

            //assert
            queryNoBase.Should().Contain("&filter.titleFilter=");
            queryNoBase.Should().Contain("&filter.descriptionFilter=");
        }

        [Test]
        public void CreateQueryDescriptionQuerySet()
        {
            //arrange
            CategoryFilter filter = new CategoryFilter(1, 10, Guid.NewGuid());
            filter.DescriptionFilter = "description";

            //act
            string queryNoBase = filter.CreateQuery(false);

            //assert
            queryNoBase.Should().Contain("&filter.descriptionFilter=");
        }

        [Test]
        public void CreateQueryTitleQuerySet()
        {
            //arrange
            CategoryFilter filter = new CategoryFilter(1, 10, Guid.NewGuid());
            filter.TitleFilter = "title";

            //act
            string queryNoBase = filter.CreateQuery(false);

            //assert
            queryNoBase.Should().Contain("&filter.titleFilter=");
        }
    }
}