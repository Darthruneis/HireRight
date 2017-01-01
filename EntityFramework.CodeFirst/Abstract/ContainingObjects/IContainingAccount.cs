using System;
using HireRight.EntityFramework.CodeFirst.Models.OrderAggregate;

namespace HireRight.EntityFramework.CodeFirst.Abstract.ContainingObjects
{
    public interface IContainingAccount
    {
        Account Account { get; set; }
        Guid AccountId { get; set; }
    }
}