using System.Collections.Generic;
using FluentAssertions;
using HireRight.Persistence.Models.CompanyAggregate;
using NUnit.Framework;

namespace HireRight.BusinessLogic.Tests
{
    [TestFixture]
    public class ProductTests
    {
        [TestCase(1, null)]
        [TestCase(9, null)]
        [TestCase(10, 10)]
        [TestCase(11, 10)]
        [TestCase(15, 10)]
        [TestCase(24, 10)]
        [TestCase(25, 25)]
        [TestCase(26, 25)]
        [TestCase(49, 25)]
        [TestCase(50, 50)]
        [TestCase(51, 50)]
        [TestCase(99, 50)]
        [TestCase(100, 100)]
        [TestCase(101, 100)]
        [TestCase(999, 100)]
        [TestCase(1000, 1000)]
        [TestCase(1001, 1000)]
        public void GetHighestDiscountForQuantityTests(long quantity, int? expectedThreshold)
        {
            var discounts = new List<Discount>
                            {
                                new Discount(false, 1.00m, 10, 1, 1),
                                new Discount(false, 2.00m, 25, 1, 1),
                                new Discount(false, 3.00m, 50, 1, 1),
                                new Discount(false, 4.00m, 100, 1, 1),
                                new Discount(false, 5.00m, 1000, 1, 1),
                            };
            var product = new Product("Fake", 100.00m, 1, discounts);

            var result = product.GetHighestDiscountForQuantity(quantity);

            if (expectedThreshold.HasValue)
                result.Threshold.Should().Be(expectedThreshold);
            else
                result.Should().BeNull();
        }

        [TestCase(1, null)]
        [TestCase(9, null)]
        [TestCase(10, 10)]
        [TestCase(11, 10)]
        [TestCase(15, 10)]
        [TestCase(24, 10)]
        [TestCase(25, 25)]
        [TestCase(26, 25)]
        [TestCase(49, 25)]
        [TestCase(50, 25)]
        [TestCase(51, 25)]
        [TestCase(99, 25)]
        [TestCase(100, 100)]
        [TestCase(101, 100)]
        [TestCase(999, 100)]
        [TestCase(1000, 1000)]
        [TestCase(1001, 1000)]
        public void GetHighestDiscountForQuantityIgnoresInactiveDiscountsTests(long quantity, int? expectedThreshold)
        {
            var discounts = new List<Discount>
                            {
                                new Discount(false, 1.00m, 10, 1, 1),
                                new Discount(false, 2.00m, 25, 1, 1),
                                new Discount(false, 3.00m, 50, 1, 1) {IsActive = false},
                                new Discount(false, 4.00m, 100, 1, 1),
                                new Discount(false, 5.00m, 1000, 1, 1),
                            };
            var product = new Product("Fake", 100.00m, 1, discounts);

            var result = product.GetHighestDiscountForQuantity(quantity);

            if (expectedThreshold.HasValue)
                result.Threshold.Should().Be(expectedThreshold);
            else
                result.Should().BeNull();
        }
    }
}