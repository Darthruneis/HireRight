using HireRight.EntityFramework.CodeFirst.Models.CompanyAggregate;

namespace HireRight.EntityFramework.CodeFirst.Abstract.ContainingObjects
{
    /// <summary>
    /// Defines an entity which contains an <see cref="Models.CompanyAggregate.Address" />.
    /// </summary>
    public interface IContainingAddress
    {
        /// <summary>
        /// The address information for this entity.
        /// </summary>
        Address Address { get; set; }
    }
}