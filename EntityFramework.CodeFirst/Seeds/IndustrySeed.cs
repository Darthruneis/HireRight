using System.Collections.Generic;
using System.Linq;
using HireRight.EntityFramework.CodeFirst.Database_Context;
using HireRight.EntityFramework.CodeFirst.Models.CompanyAggregate;

namespace HireRight.EntityFramework.CodeFirst.Seeds
{
    public class IndustrySeed
    {
        public static Industry[] Seed => GetSeed().ToArray();

        private static List<Industry> GetSeed()
        {
            return new List<Industry>()
                   {
                        new Industry(Industry.HealthCare, "Health Care"),
                       new Industry(Industry.Management, "Management"),
                       new Industry(Industry.CustomerService, "Customer Service"),
                       new Industry(Industry.Sales, "Sales"),
                       new Industry(Industry.Manufacturing, "Manufacturing"),
                       new Industry(Industry.Office, "Admin & Clerical"),
                       new Industry(Industry.General, "General"),
                       new Industry(Industry.Pharmaceutical, "Pharmaceutical") { IsActive = false},
                       new Industry(Industry.Other, "Other"),
                   };
        }
    }
}