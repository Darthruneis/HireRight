using HireRight.EntityFramework.CodeFirst.Database_Context;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HireRight.EntityFramework.CodeFirst.Models.CompanyAggregate;

namespace HireRight.Repository.Tests
{
    [TestFixture]
    public class AccountsRepositoryTests
    {
        [Test]
        public void Testing()
        {
            using (HireRightDbContext context = new HireRightDbContext())
            {
                IQueryable<Product> productQuery = context.Products.Include(x => x.Discounts);

                List<Product> prod = productQuery.ToList();

                if (!prod.Any()) Assert.Fail("Seed failed.");

                List<Discount> disc = context.Discounts.ToList();

                if (!disc.Any()) Assert.Fail("Seed failed.");
            }
        }
    }
}