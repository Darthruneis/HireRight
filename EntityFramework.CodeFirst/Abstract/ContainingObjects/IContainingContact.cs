using System;
using HireRight.EntityFramework.CodeFirst.Models.OrderAggregate;

namespace HireRight.EntityFramework.CodeFirst.Abstract.ContainingObjects
{
    public interface IContainingAdmin
    {
        Contact Admin { get; set; }
        Guid AdminContactId { get; set; }
    }

    public interface IContainingContact
    {
        Contact PrimaryContact { get; set; }
        Guid PrimaryContactId { get; set; }
    }
}