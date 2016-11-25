using HireRight.EntityFramework.CodeFirst.Abstract;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HireRight.EntityFramework.CodeFirst.Models
{
    /// <summary>
    /// A Client is a reference to an Account and up to two Contacts related to the Account.
    /// </summary>
    public class Client : PocoBase
    {
        public virtual Account Account { get; set; }

        [ForeignKey("Account")]
        public Guid AccountId { get; set; }

        public virtual Contact Admin { get; set; }

        [ForeignKey("Admin")]
        public Guid AdminContactId { get; set; }

        public virtual Contact PrimaryContact { get; set; }

        [ForeignKey("PrimaryContact")]
        public Guid PrimaryContactId { get; set; }
    }
}