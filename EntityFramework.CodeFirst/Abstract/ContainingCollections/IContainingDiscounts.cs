using HireRight.EntityFramework.CodeFirst.Models;
using System.Collections.Generic;

namespace HireRight.EntityFramework.CodeFirst.Abstract
{
    public interface IContainingDiscounts
    {
        ICollection<Discount> Discounts { get; set; }
    }
}