using HireRight.EntityFramework.CodeFirst.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HireRight.EntityFramework.CodeFirst.Models
{
    /// <summary>
    /// A company is a named relationship between a primary billing address, and a collection of clients and locations (if there are more than a single location).
    /// </summary>
    public class Company : PocoBase, IContainingClients, IContainingLocations, IContainingAddress
    {
        /// <summary>
        /// The billing address is the primary location for the business, e.g. a corporate office.
        /// </summary>
        [Required]
        public Address Address { get; set; }

        public virtual ICollection<Client> Clients { get; set; }

        public virtual ICollection<CompanyLocation> Locations { get; set; }

        [Required]
        public string Name { get; set; }
    }
}