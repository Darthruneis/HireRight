using HireRight.EntityFramework.CodeFirst.Models;
using System;

namespace HireRight.EntityFramework.CodeFirst.Abstract
{
    public interface IContainingCompany
    {
        Company Company { get; set; }
        Guid CompanyId { get; set; }
    }
}