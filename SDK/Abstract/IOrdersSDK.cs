using DataTransferObjects.Data_Transfer_Objects;
using DataTransferObjects.Filters;
using DataTransferObjects.Filters.Concrete;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SDK.Abstract
{
    public interface IOrdersSDK
    {
        Task<OrderDetailsDTO> AddOrder(OrderDetailsDTO orderToAdd);

        Task<decimal> CalculatePrice(Guid productGuid, long quantity);

        Task<OrderDetailsDTO> GetOrder(Guid orderGuid);

        Task<List<OrderDetailsDTO>> GetOrders(OrderFilter filter);

        Task SubmitCards(IList<CategoryDTO> categories);

        Task<OrderDetailsDTO> UpdateOrder(OrderDetailsDTO orderToUpdate);
    }
}