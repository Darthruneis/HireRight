using System.Collections.Generic;
using HireRight.EntityFramework.CodeFirst.Models.OrderAggregate;

namespace HireRight.EntityFramework.CodeFirst.Abstract.ContainingCollections
{
    public interface IContainingClients
    {
        ICollection<Client> Clients { get; set; }
    }
}