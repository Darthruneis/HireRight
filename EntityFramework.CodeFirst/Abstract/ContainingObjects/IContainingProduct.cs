using System;
using HireRight.EntityFramework.CodeFirst.Models.OrderAggregate;

namespace HireRight.EntityFramework.CodeFirst.Abstract.ContainingObjects
{
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