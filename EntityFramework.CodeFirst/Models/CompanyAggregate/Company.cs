using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HireRight.Persistence.Models.Abstract;

namespace HireRight.Persistence.Models.CompanyAggregate
{
    /// <summary>
    /// A company is a named relationship between a primary billing address, and a collection of clients and locations (if there are more than a single location).
    /// </summary>
    public class Company : PocoBase
    {
        /// <summary>
        /// The billing address is the primary location for the business, e.g. a corporate office.
        /// </summary>
        [Required]
        public Address Address { get; set; }

        /// <summary>
        /// The set of clients related to this company.
        /// </summary>
        public virtual ICollection<Contact> Contacts { get; set; }

        /// <summary>
        /// The set of locations this company uses to do business.
        /// </summary>
        public virtual ICollection<CompanyLocation> Locations { get; set; }

        /// <summary>
        /// The name of this company.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Any notes about this account.
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Order history for this account.
        /// </summary>
        public virtual ICollection<Order> Orders { get; set; }

        public Company()
        {
            Orders = new List<Order>();
            Locations = new List<CompanyLocation>();
            Contacts = new List<Contact>();
        }
    }
}