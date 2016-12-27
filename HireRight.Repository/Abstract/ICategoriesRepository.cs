using DataTransferObjects.Filters.Concrete;
using HireRight.EntityFramework.CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HireRight.Repository.Abstract
{
    public interface ICategoriesRepository
    {
        Task<ScaleCategory> Add(ScaleCategory itemToAdd);

        Task<List<ScaleCategory>> Get();

        Task<ScaleCategory> Get(Guid itemGuid);

        Task<ScaleCategory> Update(ScaleCategory itemToUpdate);
    }
}