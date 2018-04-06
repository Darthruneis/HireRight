using System;
using System.Threading.Tasks;
using DataTransferObjects.Filters.Concrete;
using HireRight.Persistence.Models;
using HireRight.Persistence.Models.CompanyAggregate;

namespace HireRight.Persistence.Abstract
{
    public interface IDiscountsRepository
    {
        Task<Discount> Add(Discount itemToAdd);

        Task<PageResult<Discount>> Get(DiscountFilter filter);

        Task<Discount> Get(Guid itemGuid);

        Task<Discount> Update(Discount itemToUpdate);
    }
}