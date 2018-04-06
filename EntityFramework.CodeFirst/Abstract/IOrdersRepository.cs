using System;
using System.Threading.Tasks;
using DataTransferObjects.Filters.Concrete;
using HireRight.Persistence.Models;
using HireRight.Persistence.Models.CompanyAggregate;

namespace HireRight.Persistence.Abstract
{
    public interface IOrdersRepository
    {
        Task<Order> Add(Order itemToAdd);

        Task<PageResult<Order>> Get(OrderFilter filter);

        Task<Order> Get(Guid itemGuid);

        Task<Order> Update(Order itemToUpdate);
    }
}