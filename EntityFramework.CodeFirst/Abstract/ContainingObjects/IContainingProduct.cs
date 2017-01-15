using System;
using HireRight.EntityFramework.CodeFirst.Models.CompanyAggregate;

namespace HireRight.EntityFramework.CodeFirst.Abstract.ContainingObjects
{
    /// <summary>
    /// Defines an entity which contains a navigation property for a <see cref="Models.CompanyAggregate.Product" />
    /// </summary>
    public interface IContainingProduct
    {
        /// <summary>
        /// The Product that is being purchased.
        /// </summary>
        Product Product { get; set; }

        /// <summary>
        /// Foreign key for the Product.
        /// </summary>
        Guid ProductId { get; set; }
    }
}