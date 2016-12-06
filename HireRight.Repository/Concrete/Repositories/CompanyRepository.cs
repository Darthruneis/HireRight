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
    public class CompanyRepository : ICompanyRepository
    {
        private readonly IRepositoryBase<Company> _repositoryBase;

        public CompanyRepository(IRepositoryBase<Company> repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }

        public async Task<Company> Add(Company itemToAdd)
        {
            using (HireRightDbContext context = new HireRightDbContext())
            {
                return await _repositoryBase.AddBase(itemToAdd, context.Companies).ConfigureAwait(false);
            }
        }

        public async Task<List<Company>> Get(CompanyFilter filter)
        {
            List<Company> companies;

            using (HireRightDbContext context = new HireRightDbContext())
            {
                IQueryable<Company> companiesQuery = context.Companies.Include(x => x.Clients).Include(x => x.Locations);

                companiesQuery = companiesQuery.FilterByClients(filter.ClientFilter)
                                               .FilterByLocations(filter.LocationFilter)
                                               .FilterByAddress(filter.BillingAddressFilter)
                                               .Where(x => string.IsNullOrWhiteSpace(filter.Name)
                                                           || x.Name.Contains(filter.Name));

                companies = await _repositoryBase.TakePage(companiesQuery, filter).ConfigureAwait(false);
            }

            return companies;
        }

        public async Task<Company> Get(Guid itemGuid)
        {
            Company company;

            using (HireRightDbContext context = new HireRightDbContext())
            {
                company = await _repositoryBase.GetBase(itemGuid, context.Companies.Include(x => x.Clients).Include(x => x.Locations)).ConfigureAwait(false);
            }

            return company;
        }

        public async Task<Company> Update(Company itemToUpdate)
        {
            using (HireRightDbContext context = new HireRightDbContext())
            {
                return await _repositoryBase.UpdateBase(itemToUpdate, context.Companies).ConfigureAwait(false);
            }
        }
    }
}