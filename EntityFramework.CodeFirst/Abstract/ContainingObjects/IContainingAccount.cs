using HireRight.EntityFramework.CodeFirst.Models;
using System;

namespace HireRight.EntityFramework.CodeFirst.Abstract
{
    public interface IContainingAccount
    {
        Account Account { get; set; }
        Guid AccountId { get; set; }
    }
}