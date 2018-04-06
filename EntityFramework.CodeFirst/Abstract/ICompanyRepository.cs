using System;
using System.Threading.Tasks;
using DataTransferObjects.Filters.Concrete;
using HireRight.Persistence.Models;
using HireRight.Persistence.Models.CompanyAggregate;

namespace HireRight.Persistence.Abstract
{
    public interface ICompanyRepository
    {
        Task<Company> Add(Company itemToAdd);

        Task<PageResult<Company>> Get(CompanyFilter filter);

        Task<Company> Get(Guid itemGuid);

        Task<Company> Update(Company itemToUpdate);
    }
}