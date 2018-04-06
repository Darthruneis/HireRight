using System;
using System.Threading.Tasks;
using DataTransferObjects.Filters.Concrete;
using HireRight.Persistence.Models;
using HireRight.Persistence.Models.CompanyAggregate;

namespace HireRight.Persistence.Abstract
{
    public interface IContactsRepository
    {
        Task<Contact> Add(Contact itemToAdd);

        Task<PageResult<Contact>> Get(ContactFilter filter);

        Task<Contact> Get(Guid itemGuid);

        Task<Contact> Update(Contact itemToUpdate);
    }
}