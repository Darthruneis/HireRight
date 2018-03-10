using System.Collections.Generic;
using HireRight.EntityFramework.CodeFirst.Models.CompanyAggregate;

namespace HireRight.EntityFramework.CodeFirst.Seeds
{
    internal static class ProductsSeed
    {
        internal static List<Product> Seed()
        {
            var discounts = new List<Discount>
            {
                new Discount(false, 05.00m, 50),
                new Discount(false, 10.00m, 100),
                new Discount(false, 25.00m, 200),
                new Discount(false, 32.00m, 500)
            };

            Product product = new Product("Assessment Test", 50.00m, Product.AssessmentTest, discounts);

            return new List<Product>() { product };
        }
    }
}