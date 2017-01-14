﻿using HireRight.EntityFramework.CodeFirst.Abstract;
using HireRight.EntityFramework.CodeFirst.Abstract.ContainingCollections;
using HireRight.EntityFramework.CodeFirst.Abstract.ContainingObjects;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HireRight.EntityFramework.CodeFirst.Models.OrderAggregate
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

        /// <summary>
        /// The set of clients related to this company.
        /// </summary>
        public virtual ICollection<Client> Clients { get; set; }

        /// <summary>
        /// The set of locations this company uses to do business.
        /// </summary>
        public virtual ICollection<CompanyLocation> Locations { get; set; }

        /// <summary>
        /// The name of this company.
        /// </summary>
        [Required]
        public string Name { get; set; }
    }
}