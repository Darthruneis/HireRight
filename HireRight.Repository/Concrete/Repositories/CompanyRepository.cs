using DataTransferObjects.Filters.Concrete;
using HireRight.EntityFramework.CodeFirst.Database_Context;
using HireRight.EntityFramework.CodeFirst.Models.CompanyAggregate;
using HireRight.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using HireRight.EntityFramework.CodeFirst.Models;

namespace HireRight.Repository.Concrete
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository() : base(() => new HireRightDbContext())
        {
        }

        public CompanyRepository(Func<HireRightDbContext> contextFunc) : base(contextFunc)
        {
        }

        public async Task<Company> Add(Company itemToAdd)
        {
            using (HireRightDbContext context = ContextFunc.Invoke())
            {
                return await AddBase(itemToAdd, context.Companies, context).ConfigureAwait(false);
            }
        }

        public async Task<PageResult<Company>> Get(CompanyFilter filter)
        {
            using (HireRightDbContext context = ContextFunc.Invoke())
            {
                IQueryable<Company> companiesQuery = context.Companies.Include(x => x.Locations).Include(x => x.Contacts).Include(x => x.Orders);

                if (!string.IsNullOrWhiteSpace(filter.Name))
                    companiesQuery = companiesQuery.Where(x => x.Name.Contains(filter.Name));

                var companies = await TakePage(companiesQuery, filter).ConfigureAwait(false);
                return companies;
            }
        }

        public async Task<Company> Get(Guid itemGuid)
        {
            using (HireRightDbContext context = ContextFunc.Invoke())
            {
                Company company = await GetBase(itemGuid, context.Companies.Include(x => x.Locations)).ConfigureAwait(false);
                return company;
            }
        }

        public async Task<Company> Update(Company itemToUpdate)
        {
            using (HireRightDbContext context = ContextFunc.Invoke())
            {
                return await UpdateBase(itemToUpdate, context.Companies, context).ConfigureAwait(false);
            }
        }
    }
}