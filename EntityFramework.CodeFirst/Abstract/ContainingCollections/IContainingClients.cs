using HireRight.EntityFramework.CodeFirst.Models;
using System.Collections.Generic;

namespace HireRight.EntityFramework.CodeFirst.Abstract
{
    public interface IContainingClients
    {
        ICollection<Client> Clients { get; set; }
    }
}