using DataTransferObjects.Filters;
using HireRight.EntityFramework.CodeFirst.Database_Context;
using HireRight.EntityFramework.CodeFirst.Models;
using HireRight.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DataTransferObjects.Filters.Concrete;

namespace HireRight.Repository.Concrete
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly IRepositoryBase<Order> _repositoryBase;

        public OrdersRepository(IRepositoryBase<Order> repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }

        public async Task<Order> Add(Order itemToAdd)
        {
            using (HireRightDbContext context = new HireRightDbContext())
            {
                return await _repositoryBase.AddBase(itemToAdd, context.Orders).ConfigureAwait(false);
            }
        }

        public async Task<List<Order>> Get(OrderFilter filter)
        {
            List<Order> orders;

            using (HireRightDbContext context = new HireRightDbContext())
            {
                IQueryable<Order> ordersQuery = context.Orders.Include(x => x.Product).Include(x => x.Company);

                orders = await _repositoryBase.TakePage(ordersQuery, filter).ConfigureAwait(false);
            }

            return orders;
        }

        public async Task<Order> Get(Guid itemGuid)
        {
            Order order;

            using (HireRightDbContext context = new HireRightDbContext())
            {
                order = await _repositoryBase.GetBase(itemGuid, context.Orders.Include(x => x.Product).Include(x => x.Company)).ConfigureAwait(false);
            }

            return order;
        }

        public async Task<Order> Update(Order itemToUpdate)
        {
            using (HireRightDbContext context = new HireRightDbContext())
            {
                return await _repositoryBase.UpdateBase(itemToUpdate, context.Orders).ConfigureAwait(false);
            }
        }
    }
}