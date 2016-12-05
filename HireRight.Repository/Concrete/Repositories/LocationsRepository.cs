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
    public class LocationsRepository : ILocationsRepository
    {
        private readonly IRepositoryBase<CompanyLocation> _repositoryBase;

        public LocationsRepository(IRepositoryBase<CompanyLocation> repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }

        public async Task<CompanyLocation> Add(CompanyLocation itemToAdd)
        {
            using (HireRightDbContext context = new HireRightDbContext())
            {
                return await _repositoryBase.AddBase(itemToAdd, context.Locations).ConfigureAwait(false);
            }
        }

        public async Task<List<CompanyLocation>> Get(LocationFilter filter)
        {
            List<CompanyLocation> locations;

            using (HireRightDbContext context = new HireRightDbContext())
            {
                IQueryable<CompanyLocation> locationsQuery = context.Locations.Include(x => x.Company);

                locationsQuery = locationsQuery.FilterByCompany(filter.CompanyFilter);

                locations = await _repositoryBase.TakePage(locationsQuery, filter).ConfigureAwait(false);
            }

            return locations;
        }

        public async Task<CompanyLocation> Get(Guid itemGuid)
        {
            CompanyLocation location;

            using (HireRightDbContext context = new HireRightDbContext())
            {
                location = await _repositoryBase.GetBase(itemGuid, context.Locations.Include(x => x.Company)).ConfigureAwait(false);
            }

            return location;
        }

        public async Task<CompanyLocation> Update(CompanyLocation itemToUpdate)
        {
            using (HireRightDbContext context = new HireRightDbContext())
            {
                return await _repositoryBase.UpdateBase(itemToUpdate, context.Locations).ConfigureAwait(false);
            }
        }
    }
}