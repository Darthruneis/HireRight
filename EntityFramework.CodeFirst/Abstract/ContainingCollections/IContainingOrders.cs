using HireRight.EntityFramework.CodeFirst.Models.OrderAggregate;
using System.Collections.Generic;

namespace HireRight.EntityFramework.CodeFirst.Abstract.ContainingCollections
{
    /// <summary>
    /// Defines an entity which contains a navigation collection for a set of <see cref="Order" /> s.
    /// </summary>
    public interface IContainingOrders
    {
        /// <summary>
        /// The set of <see cref="Order" /> s related to this entity.
        /// </summary>
        ICollection<Order> Orders { get; set; }
    }
}