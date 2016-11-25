using HireRight.EntityFramework.CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataTransferObjects.Filters;
using DataTransferObjects.Filters.Concrete;

namespace HireRight.Repository.Abstract
{
    public interface IDiscountsRepository
    {
        Task<Discount> Add(Discount itemToAdd);

        Task<List<Discount>> Get(DiscountFilter filter);

        Task<Discount> Get(Guid itemGuid);

        Task<Discount> Update(Discount itemToUpdate);
    }
}