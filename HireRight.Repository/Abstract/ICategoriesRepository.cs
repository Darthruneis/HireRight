using DataTransferObjects.Filters.Concrete;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HireRight.EntityFramework.CodeFirst.Models.CompanyAggregate;

namespace HireRight.Repository.Abstract
{
    public interface ICategoriesRepository
    {
        Task<ScaleCategory> Add(ScaleCategory itemToAdd);

        Task<List<ScaleCategory>> Get(CategoryFilter filter);

        Task<ScaleCategory> Get(Guid itemGuid);

        Task<ScaleCategory> Update(ScaleCategory itemToUpdate);
    }
}