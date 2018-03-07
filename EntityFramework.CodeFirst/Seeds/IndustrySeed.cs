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
                        new Industry(Industry.Medical, "Medical"),
                       new Industry(Industry.Management, "Management"),
                       new Industry(Industry.CustomerServiceSales, "Customer Service & Sales"),
                       new Industry(Industry.Startup, "Startup"),
                       new Industry(Industry.Manufacturing, "Manufacturing"),
                       new Industry(Industry.AdministrationClerical, "Administration and Clerical"),
                       new Industry(Industry.General, "General"),
                       new Industry(Industry.ProfessionalServices, "Professional Services"),
                       new Industry(Industry.Other, "Other Industries"),
                   };
        }
    }
}