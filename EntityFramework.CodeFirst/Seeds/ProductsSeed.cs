using System.Collections.Generic;
using HireRight.EntityFramework.CodeFirst.Models.OrderAggregate;

namespace HireRight.EntityFramework.CodeFirst.Seeds
{
    internal static class ProductsSeed
    {
        internal static List<Product> Seed()
        {
            List<Discount> discounts = new List<Discount>
            {
                new Discount(false, 2.00m, 50),
                new Discount(true, 0.20m, 100),
                new Discount(false, 8.00m, 500),
                new Discount(false, 10.00m, 10000)
            };

            Product product = new Product("Assessment Test", 25.00m, discounts);

            return new List<Product>() { product };
        }
    }
}