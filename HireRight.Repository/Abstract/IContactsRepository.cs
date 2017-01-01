using HireRight.EntityFramework.CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataTransferObjects.Filters;
using DataTransferObjects.Filters.Concrete;
using HireRight.EntityFramework.CodeFirst.Models.OrderAggregate;

namespace HireRight.Repository.Abstract
{
    public interface IContactsRepository
    {
        Task<Contact> Add(Contact itemToAdd);

        Task<List<Contact>> Get(ContactFilter filter);

        Task<Contact> Get(Guid itemGuid);

        Task<Contact> Update(Contact itemToUpdate);
    }
}