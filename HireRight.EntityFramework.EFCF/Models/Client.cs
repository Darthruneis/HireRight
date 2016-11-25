using System.ComponentModel.DataAnnotations.Schema;
using HireRight.EntityFramework.EFCF.Abstract;

namespace HireRight.EntityFramework.EFCF.Models
{
    /// <summary>
    ///     A Client is a reference to an Account and up to two Contacts related to the Account.
    /// </summary>
    public class Client : PocoBase
    {
        public virtual Account Account { get; set; }

        [ForeignKey("Account")]
        public int AccountId { get; set; }

        public virtual Contact Admin { get; set; }

        [ForeignKey("Admin")]
        public int AdminContactId { get; set; }

        public virtual Contact PrimaryContact { get; set; }

        [ForeignKey("PrimaryContact")]
        public int PrimaryContactId { get; set; }
    }
}