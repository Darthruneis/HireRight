using System.Collections.Generic;
using HireRight.EntityFramework.CodeFirst.Models.OrderAggregate;

namespace HireRight.EntityFramework.CodeFirst.Abstract.ContainingCollections
{
    public interface IContainingDiscounts
    {
        ICollection<Discount> Discounts { get; set; }
    }
}