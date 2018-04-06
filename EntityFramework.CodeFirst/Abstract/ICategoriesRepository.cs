using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataTransferObjects.Filters.Concrete;
using HireRight.Persistence.Models;
using HireRight.Persistence.Models.CompanyAggregate;

namespace HireRight.Persistence.Abstract
{
    public interface ICategoriesRepository
    {
        Task<ScaleCategory> Add(ScaleCategory itemToAdd);

        Task<PageResult<ScaleCategory>> Get(CategoryFilter filter);

        Task<ScaleCategory> Get(Guid itemGuid);

        Task<ScaleCategory> Update(ScaleCategory itemToUpdate);

        Task<ICollection<ScaleCategory>> GetAll();
    }
}