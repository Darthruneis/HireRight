using System;
using HireRight.EntityFramework.CodeFirst.Models.OrderAggregate;

namespace HireRight.EntityFramework.CodeFirst.Abstract.ContainingObjects
{
    public interface IContainingClient
    {
        Client Client { get; set; }
        Guid ClientId { get; set; }
    }
}