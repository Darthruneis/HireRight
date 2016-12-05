using HireRight.EntityFramework.CodeFirst.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HireRight.EntityFramework.CodeFirst.Abstract
{
    public interface IContainingClient
    {
        Client Client { get; set; }
        Guid ClientId { get; set; }
    }
}