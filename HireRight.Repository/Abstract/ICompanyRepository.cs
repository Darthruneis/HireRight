using HireRight.EntityFramework.CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataTransferObjects.Filters;
using DataTransferObjects.Filters.Concrete;
using HireRight.EntityFramework.CodeFirst.Models.CompanyAggregate;

namespace HireRight.Repository.Abstract
{
    public interface ICompanyRepository
    {
        Task<Company> Add(Company itemToAdd);

        Task<List<Company>> Get(CompanyFilter filter);

        Task<Company> Get(Guid itemGuid);

        Task<Company> Update(Company itemToUpdate);
    }
}