using HireRight.EntityFramework.CodeFirst.Models.OrderAggregate;
using System;

namespace HireRight.EntityFramework.CodeFirst.Abstract.ContainingObjects
{
    /// <summary>
    /// Defines an entity which contains an <see cref="HireRight.EntityFramework.CodeFirst.Models.OrderAggregate.Account" /> navigation property.
    /// </summary>
    public interface IContainingAccount
    {
        /// <summary>
        /// The account related to this entity.
        /// </summary>
        Account Account { get; set; }

        /// <summary>
        /// Foreign key for the account related to this entity.
        /// </summary>
        Guid AccountId { get; set; }
    }
}