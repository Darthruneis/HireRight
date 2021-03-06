﻿using DataTransferObjects.Data_Transfer_Objects;
using DataTransferObjects.Filters.Concrete;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HireRight.EntityFramework.CodeFirst.Models.CompanyAggregate;

namespace HireRight.BusinessLogic.Abstract
{
    public interface IOrdersBusinessLogic : IBusinessLogicBase<Order, OrderDetailsDTO>
    {
        Task<OrderDetailsDTO> Add(OrderDetailsDTO objectDto);

        Task<decimal> CalculatePrice(Guid productGuid, long quantity);

        Task CreateOrder(NewOrderDTO newOrder);

        Task<OrderDetailsDTO> Get(Guid orderGuid);

        Task<List<OrderDetailsDTO>> Get(OrderFilter filter);

        void SubmitCards(SubmitCardsDTO cardsToSubmit);

        Task<OrderDetailsDTO> Update(OrderDetailsDTO orderDto);
    }
}