using System.Collections.Generic;
using HireRight.EntityFramework.CodeFirst.Models.CompanyAggregate;

namespace HireRight.EntityFramework.CodeFirst.Abstract.ContainingCollections
{
    /// <summary>
    /// Defines an entity which contains a collection navigation property for a set of <see cref="Discount" /> s.
    /// </summary>
    public interface IContainingDiscounts
    {
        /// <summary>
        /// The set of discounts that are related to this entity.
        /// </summary>
        ICollection<Discount> Discounts { get; set; }
    }
}