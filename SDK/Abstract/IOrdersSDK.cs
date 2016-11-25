using DataTransferObjects.Data_Transfer_Objects;
using DataTransferObjects.Filters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataTransferObjects.Filters.Concrete;

namespace SDK.Abstract
{
    public interface IOrdersSDK
    {
        Task<OrderDetailsDTO> AddOrder(OrderDetailsDTO orderToAdd);

        Task<decimal> CalculatePrice(Guid productGuid, long quantity);

        Task<OrderDetailsDTO> GetOrder(Guid orderGuid);

        Task<List<OrderDetailsDTO>> GetOrders(OrderFilter filter);

        Task<OrderDetailsDTO> UpdateOrder(OrderDetailsDTO orderToUpdate);
    }
}