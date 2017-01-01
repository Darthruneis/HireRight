using HireRight.EntityFramework.CodeFirst.Models.OrderAggregate;
using System;

namespace HireRight.EntityFramework.CodeFirst.Abstract.ContainingObjects
{
    /// <summary>
    /// Defines an entity which contains a navigation property for an Administrator <see cref="Contact" />.
    /// </summary>
    public interface IContainingAdmin
    {
        /// <summary>
        /// The administrator contact for this entity.
        /// </summary>
        Contact Admin { get; set; }

        /// <summary>
        /// Foreign key for the administrator contact for this entity.
        /// </summary>
        Guid AdminContactId { get; set; }
    }

    /// <summary>
    /// Defines an entity which contains a navigation property for a Primary <see cref="Contact" />.
    /// </summary>
    public interface IContainingContact
    {
        /// <summary>
        /// The primary contact for this entity.
        /// </summary>
        Contact PrimaryContact { get; set; }

        /// <summary>
        /// Foreign key for the primary contact for this entity.
        /// </summary>
        Guid PrimaryContactId { get; set; }
    }
}