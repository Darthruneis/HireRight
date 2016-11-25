using HireRight.EntityFramework.CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataTransferObjects.Filters;
using DataTransferObjects.Filters.Concrete;

namespace HireRight.Repository.Abstract
{
    public interface IAccountsRepository
    {
        Task<Account> Add(Account itemToAdd);

        Task<List<Account>> Get(AccountFilter filter);

        Task<Account> Get(Guid itemGuid);

        Task<Account> Update(Account itemToUpdate);
    }
}