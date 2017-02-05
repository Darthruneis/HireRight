using System;
using System.Collections.Generic;
using DataTransferObjects.Data_Transfer_Objects;
using DataTransferObjects.Filters.Abstract;
using DataTransferObjects.Filters.Concrete;
using FluentAssertions;
using FluentAssertions.Common;
using NUnit.Framework;

namespace DataTransferTests
{
    [TestFixture]
    [Category("Filter")]
    public class FilterTests
    {
        [TestCase(1, TestName = "1 Guid Test")]
        [TestCase(2, TestName = "2 Guids Test")]
        [TestCase(10, TestName = "10 Guids Test")]
        [TestCase(20, TestName = "20 Guids Test")]
        public void CreateQueryWithGuids(int count)
        {
            //arrange
            List<Guid> guids = new List<Guid>();
            for (int i = 0; i < count; i++)
                guids.Add(Guid.NewGuid());

            guids.Count.Should().IsSameOrEqualTo(count);
            Filter<CategoryDTO> filter = new CategoryFilter(1, 10, guids.ToArray());

            //act
            string query = filter.CreateQuery(true);

            //assert
            query.Should().Contain("&filter.itemGuids=");
            foreach (Guid guid in guids)
                query.Should().Contain(guid.ToString());
        }
    }
}