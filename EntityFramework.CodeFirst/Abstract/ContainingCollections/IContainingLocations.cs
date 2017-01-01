using HireRight.EntityFramework.CodeFirst.Models.OrderAggregate;
using System.Collections.Generic;

namespace HireRight.EntityFramework.CodeFirst.Abstract.ContainingCollections
{
    /// <summary>
    /// Defines an entity which contains a collection navigation property for a set of <see cref="CompanyLocation" /> s.
    /// </summary>
    public interface IContainingLocations
    {
        /// <summary>
        /// The set of Company Loccations related to this entity.
        /// </summary>
        ICollection<CompanyLocation> Locations { get; set; }
    }
}