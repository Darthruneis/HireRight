using HireRight.EntityFramework.CodeFirst.Models;
using System.Collections.Generic;

namespace HireRight.EntityFramework.CodeFirst.Abstract
{
    public interface IContainingOrders
    {
        ICollection<Order> Orders { get; set; }
    }
}