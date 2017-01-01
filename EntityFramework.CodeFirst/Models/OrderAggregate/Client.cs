using HireRight.EntityFramework.CodeFirst.Abstract;
using HireRight.EntityFramework.CodeFirst.Abstract.ContainingObjects;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HireRight.EntityFramework.CodeFirst.Models.OrderAggregate
{
    /// <summary>
    /// A Client is a reference to an Account and up to two Contacts related to the Account.
    /// </summary>
    public class Client : PocoBase, IContainingAccount, IContainingAdmin, IContainingContact
    {
        /// <summary>
        /// The account this client is part of.
        /// </summary>
        public virtual Account Account { get; set; }

        /// <summary>
        /// Foreign key for the account this client is part of.
        /// </summary>
        [ForeignKey("Account")]
        public Guid AccountId { get; set; }

        /// <summary>
        /// The Administrator contact for this client.
        /// </summary>
        public virtual Contact Admin { get; set; }

        /// <summary>
        /// Foreign key for the administrator contact for this client.
        /// </summary>
        [ForeignKey("Admin")]
        public Guid AdminContactId { get; set; }

        /// <summary>
        /// The primary contact for this client.
        /// </summary>
        public virtual Contact PrimaryContact { get; set; }

        /// <summary>
        /// Foreign key for the primary contact for this client.
        /// </summary>
        [ForeignKey("PrimaryContact")]
        public Guid PrimaryContactId { get; set; }
    }
}