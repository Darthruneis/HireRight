using DataTransferObjects.Filters;
using DataTransferObjects.Filters.Concrete;
using HireRight.EntityFramework.CodeFirst.Database_Context;
using HireRight.EntityFramework.CodeFirst.Models;
using HireRight.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace HireRight.Repository.Concrete
{
    public class AccountsRepository : IAccountsRepository
    {
        private readonly IRepositoryBase<Account> _repositoryBase;

        public AccountsRepository(IRepositoryBase<Account> repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }

        public async Task<Account> Add(Account itemToAdd)
        {
            using (HireRightDbContext context = new HireRightDbContext())
            {
                return await _repositoryBase.AddBase(itemToAdd, context.Accounts).ConfigureAwait(false);
            }
        }

        public async Task<List<Account>> Get(AccountFilter filter)
        {
            List<Account> accountsFound;

            using (HireRightDbContext context = new HireRightDbContext())
            {
                IQueryable<Account> accountsQuery = context.Accounts.Include(x => x.Company).Include(x => x.Clients);

                accountsQuery = accountsQuery.FilterByCompany(filter.CompanyFilter)
                                             .Where(x => string.IsNullOrWhiteSpace(filter.Notes)
                                                         || x.Notes.Contains(filter.Notes));

                accountsFound = await _repositoryBase.TakePage(accountsQuery, filter).ConfigureAwait(false);
            }

            return accountsFound;
        }

        public async Task<Account> Get(Guid itemGuid)
        {
            Account account;

            using (HireRightDbContext context = new HireRightDbContext())
            {
                account = await _repositoryBase.GetBase(itemGuid, context.Accounts.Include(x => x.Company).Include(x => x.Clients)).ConfigureAwait(false);
            }

            return account;
        }

        public async Task<Account> Update(Account itemToUpdate)
        {
            using (HireRightDbContext context = new HireRightDbContext())
            {
                return await _repositoryBase.UpdateBase(itemToUpdate, context.Accounts).ConfigureAwait(false);
            }
        }
    }
}