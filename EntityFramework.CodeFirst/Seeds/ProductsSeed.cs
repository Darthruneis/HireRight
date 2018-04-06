using System.Collections.Generic;
using HireRight.Persistence.Models.CompanyAggregate;

namespace HireRight.Persistence.Seeds
{
    internal static class ProductsSeed
    {
        internal static List<Product> Seed()
        {
            Product product = new Product("Assessment Units", 50.00m, Product.AssessmentTest);

            return new List<Product>() { product };
        }

        internal static List<Discount> DiscountSeed()
        {
            return new List<Discount>
                   {
                       new Discount(false, 5.00m, 50, Product.AssessmentTest, 1),
                       new Discount(false, 15.00m, 100, Product.AssessmentTest, 2),
                       new Discount(false, 25.00m, 200, Product.AssessmentTest, 3),
                       new Discount(false, 34.00m, 500, Product.AssessmentTest, 4)
                   };
        }
    }
}