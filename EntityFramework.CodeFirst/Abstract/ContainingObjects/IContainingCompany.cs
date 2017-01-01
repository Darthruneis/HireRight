using System;
using HireRight.EntityFramework.CodeFirst.Models.OrderAggregate;

namespace HireRight.EntityFramework.CodeFirst.Abstract.ContainingObjects
{
    public interface IContainingCompany
    {
        Company Company { get; set; }
        Guid CompanyId { get; set; }
    }
}