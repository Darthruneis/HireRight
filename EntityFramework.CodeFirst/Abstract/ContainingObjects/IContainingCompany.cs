using HireRight.EntityFramework.CodeFirst.Models.OrderAggregate;
using System;

namespace HireRight.EntityFramework.CodeFirst.Abstract.ContainingObjects
{
    /// <summary>
    /// Defines an entity which contains a <see cref="Models.OrderAggregate.Company" /> navigation property.
    /// </summary>
    public interface IContainingCompany
    {
        /// <summary>
        /// The company that is related to this entity.
        /// </summary>
        Company Company { get; set; }

        /// <summary>
        /// Foreign key for the company related to this entity.
        /// </summary>
        Guid CompanyId { get; set; }
    }
}