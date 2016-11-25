using HireRight.EntityFramework.EFCF.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace HireRight.EntityFramework.EFCF.Database_Context
{
    public class HireRightDbContext : DbContext
    {
        public virtual DbSet<Account> Accounts { get; set; }

        public virtual DbSet<Client> Clients { get; set; }

        public virtual DbSet<Company> Companies { get; set; }

        public virtual DbSet<Contact> Contacts { get; set; }

        public virtual DbSet<Discount> Discounts { get; set; }

        public virtual DbSet<CompanyLocation> Locations { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public HireRightDbContext() : base("HireRightDb")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            ConfigureConventions(modelBuilder);
        }

        private static void ConfigureConventions(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }
    }
}