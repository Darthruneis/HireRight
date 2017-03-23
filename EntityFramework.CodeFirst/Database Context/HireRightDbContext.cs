using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using HireRight.EntityFramework.CodeFirst.Models.CompanyAggregate;

namespace HireRight.EntityFramework.CodeFirst.Database_Context
{
    /// <summary>
    /// Database context class for the Hire Right Database.
    /// </summary>
    public class HireRightDbContext : DbContext
    {
        /// <summary>
        /// The <see cref="ScaleCategory" /> table in the database.
        /// </summary>
        public virtual DbSet<ScaleCategory> Categories { get; set; }

        /// <summary>
        /// The <see cref="Company" /> table in the database.
        /// </summary>
        public virtual DbSet<Company> Companies { get; set; }

        /// <summary>
        /// The <see cref="Contact" /> table in the database.
        /// </summary>
        public virtual DbSet<Contact> Contacts { get; set; }

        /// <summary>
        /// The <see cref="Discount" /> table in the database.
        /// </summary>
        public virtual DbSet<Discount> Discounts { get; set; }

        /// <summary>
        /// The <see cref="CompanyLocation" /> table in the database.
        /// </summary>
        public virtual DbSet<CompanyLocation> Locations { get; set; }

        /// <summary>
        /// The <see cref="Order" /> table in the database.
        /// </summary>
        public virtual DbSet<Order> Orders { get; set; }

        /// <summary>
        /// The <see cref="Product" /> table in the database.
        /// </summary>
        public virtual DbSet<Product> Products { get; set; }

        /// <summary>
        /// Initializes the context based off the connection string name set in the class.
        /// </summary>
        public HireRightDbContext() : base("name=HireRightDb")
        { }

        /// <summary>
        /// Defines how the context creates models from the database information.
        /// </summary>
        /// <param name="modelBuilder">The model builder to be used.</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }
    }
}