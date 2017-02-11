using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataTransferObjects.Filters.Concrete;
using HireRight.EntityFramework.CodeFirst.Models.CompanyAggregate;

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