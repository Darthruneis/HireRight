using HireRight.EntityFramework.CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataTransferObjects.Filters;
using DataTransferObjects.Filters.Concrete;

namespace HireRight.Repository.Abstract
{
    public interface IClientsRepository
    {
        Task<Client> Add(Client itemToAdd);

        Task<List<Client>> Get(ClientFilter filter);

        Task<Client> Get(Guid itemGuid);

        Task<Client> Update(Client itemToUpdate);
    }
}