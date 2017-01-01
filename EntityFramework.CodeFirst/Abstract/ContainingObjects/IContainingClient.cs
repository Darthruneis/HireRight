using HireRight.EntityFramework.CodeFirst.Models.OrderAggregate;
using System;

namespace HireRight.EntityFramework.CodeFirst.Abstract.ContainingObjects
{
    /// <summary>
    /// Defines an entity which contains a <see cref="HireRight.EntityFramework.CodeFirst.Models.OrderAggregate.Client" /> navigation property.
    /// </summary>
    public interface IContainingClient
    {
        /// <summary>
        /// </summary>
        Client Client { get; set; }

        Guid ClientId { get; set; }
    }
}