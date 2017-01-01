using HireRight.EntityFramework.CodeFirst.Abstract;
using HireRight.EntityFramework.CodeFirst.Abstract.ContainingCollections;
using HireRight.EntityFramework.CodeFirst.Abstract.ContainingObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HireRight.EntityFramework.CodeFirst.Models.OrderAggregate
{
    /// <summary>
    /// An account is a relationship between a Company and its Clients and Orders.
    /// </summary>
    public class Account : PocoBase, IContainingCompany, IContainingClients
    {
        /// <summary>
        /// The set of Clients related to this account.
        /// </summary>
        public virtual ICollection<Client> Clients { get; set; }

        /// <summary>
        /// The company related to this account.
        /// </summary>
        public virtual Company Company { get; set; }

        /// <summary>
        /// Foreign key for the company related to this account.
        /// </summary>
        [ForeignKey("Company")]
        public Guid CompanyId { get; set; }

        /// <summary>
        /// Any notes about this account.
        /// </summary>
        [Required]
        public string Notes { get; set; }

        /// <summary>
        /// Order history for this account.
        /// </summary>
        public virtual ICollection<Order> Orders { get; set; }
    }
}