using DataTransferObjects.Filters.Abstract;
using FluentAssertions;
using NUnit.Framework;

namespace DataTransferTests
{
    [Category("FilterBase")]
    [TestFixture]
    public class FilterBaseTests
    {
        private class FilterBaseForTesting : FilterBase
        {
            public FilterBaseForTesting(int page, int size) : base(page, size)
            {
            }

            public new string CreateQuery() => base.CreateQuery();
        }

        [TestCase(int.MinValue, 1, TestName = "Page INT.MINVALUE Test")]
        [TestCase(-1, 1, TestName = "Page -1 Test")]
        [TestCase(0, 1, TestName = "Page 0 Test")]
        [TestCase(1, 1, TestName = "Page 1 Test")]
        [TestCase(2, 2, TestName = "Page 2 Test")]
        [TestCase(10, 10, TestName = "Page 10 Test")]
        [TestCase(50, 50, TestName = "Page 50 Test")]
        [TestCase(100, 100, TestName = "Page 100 Test")]
        [TestCase(500, 500, TestName = "Page 500 Test")]
        [TestCase(1000, 1000, TestName = "Page 1000 Test")]
        [TestCase(int.MaxValue, int.MaxValue, TestName = "Page INT.MAXVALUE Test")]
        public void PageNumberQueryTests(int pageNumber, int expectedPage)
        {
            //arrange
            FilterBaseForTesting filterToTest = new FilterBaseForTesting(pageNumber, 10);

            //act
            string query = filterToTest.CreateQuery();

            //assert
            query.Should().Be($"?filter.pageNumber={expectedPage}&filter.pageSize=10");
        }

        [TestCase(int.MinValue, 1, TestName = "Size INT.MINVALUE Test")]
        [TestCase(-1, 1, TestName = "Size -1 Test")]
        [TestCase(0, 1, TestName = "Size 0 Test")]
        [TestCase(1, 1, TestName = "Size 1 Test")]
        [TestCase(2, 2, TestName = "Size 2 Test")]
        [TestCase(10, 10, TestName = "Size 10 Test")]
        [TestCase(50, 50, TestName = "Size 50 Test")]
        [TestCase(100, 100, TestName = "Size 100 Test")]
        [TestCase(500, 100, TestName = "Size 500 Test")]
        [TestCase(1000, 100, TestName = "Size 1000 Test")]
        [TestCase(int.MaxValue, 100, TestName = "Size INT.MAXVALUE Test")]
        public void PageSizeQueryTests(int pageSize, int expectedSize)
        {
            //arrange
            FilterBaseForTesting filterToTest = new FilterBaseForTesting(1, pageSize);

            //act
            string query = filterToTest.CreateQuery();

            //assert
            query.Should().Be($"?filter.pageNumber=1&filter.pageSize={expectedSize}");
        }
    }
}