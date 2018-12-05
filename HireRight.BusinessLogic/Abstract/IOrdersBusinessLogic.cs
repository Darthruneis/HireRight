using DataTransferObjects.Data_Transfer_Objects;
using DataTransferObjects.Filters.Concrete;
using System;
using System.Threading.Tasks;
using HireRight.Persistence.Models;
using HireRight.Persistence.Models.CompanyAggregate;

namespace HireRight.BusinessLogic.Abstract
{
    public interface IOrdersBusinessLogic : IBusinessLogicBase<Order, OrderDetailsDTO>
    {
        Task<OrderDetailsDTO> Add(OrderDetailsDTO objectDto);

        Task<decimal> CalculatePrice(Guid productGuid, long quantity);

        Task CreateOrder(NewOrderDTO newOrder);

        Task<OrderDetailsDTO> Get(Guid orderGuid);

        Task<PagingResultDTO<OrderDetailsDTO>> Get(OrderFilter filter);

        Task<Result> SubmitCards(SubmitCardsDTO cardsToSubmit);

        Task<OrderDetailsDTO> Update(OrderDetailsDTO orderDto);
    }
}