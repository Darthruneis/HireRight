using HireRight.EntityFramework.CodeFirst.Models.OrderAggregate;

namespace HireRight.EntityFramework.CodeFirst.Abstract.ContainingObjects
{
    /// <summary>
    /// Defines an entity which contains an <see cref="HireRight.EntityFramework.CodeFirst.Models.OrderAggregate.Address" />.
    /// </summary>
    public interface IContainingAddress
    {
        /// <summary>
        /// The address information for this entity.
        /// </summary>
        Address Address { get; set; }
    }
}