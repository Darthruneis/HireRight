using HireRight.EntityFramework.CodeFirst.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HireRight.EntityFramework.CodeFirst.Abstract
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