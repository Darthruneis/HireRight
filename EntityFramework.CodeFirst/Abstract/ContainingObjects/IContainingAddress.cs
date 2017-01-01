using HireRight.EntityFramework.CodeFirst.Models.OrderAggregate;

namespace HireRight.EntityFramework.CodeFirst.Abstract.ContainingObjects
{
    public interface IContainingAddress
    {
        Address Address { get; set; }
    }
}