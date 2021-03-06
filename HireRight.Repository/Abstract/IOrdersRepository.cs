﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataTransferObjects.Filters.Concrete;
using HireRight.EntityFramework.CodeFirst.Models.CompanyAggregate;

namespace HireRight.Repository.Abstract
{
    public interface IOrdersRepository
    {
        Task<Order> Add(Order itemToAdd);

        Task<List<Order>> Get(OrderFilter filter);

        Task<Order> Get(Guid itemGuid);

        Task<Order> Update(Order itemToUpdate);
    }
}