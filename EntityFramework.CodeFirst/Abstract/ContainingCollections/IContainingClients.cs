using HireRight.EntityFramework.CodeFirst.Models.OrderAggregate;
using System.Collections.Generic;

namespace HireRight.EntityFramework.CodeFirst.Abstract.ContainingCollections
{
    /// <summary>
    /// Defines an entity which contains a navigation property for a collection of <see cref="Client" /> s.
    /// </summary>
    public interface IContainingClients
    {
        /// <summary>
        /// The set of Clients related to this entity.
        /// </summary>
        ICollection<Client> Clients { get; set; }
    }
}