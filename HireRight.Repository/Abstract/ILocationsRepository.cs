using DataTransferObjects.Filters;
using HireRight.EntityFramework.CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataTransferObjects.Filters.Concrete;

namespace HireRight.Repository.Abstract
{
    public interface ILocationsRepository
    {
        Task<CompanyLocation> Add(CompanyLocation itemToAdd);

        Task<List<CompanyLocation>> Get(LocationFilter filter);

        Task<CompanyLocation> Get(Guid itemGuid);

        Task<CompanyLocation> Update(CompanyLocation itemToUpdate);
    }
}