using DataTransferObjects.Filters;
using DataTransferObjects.Filters.Concrete;
using HireRight.EntityFramework.CodeFirst.Database_Context;
using HireRight.EntityFramework.CodeFirst.Models;
using HireRight.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using HireRight.EntityFramework.CodeFirst.Models.OrderAggregate;

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
                return await _repositoryBase.AddBase(itemToAdd, context.Orders, context).ConfigureAwait(false);
            }
        }

        public async Task<List<Order>> Get(OrderFilter filter)
        {
            List<Order> orders;

            using (HireRightDbContext context = new HireRightDbContext())
            {
                IQueryable<Order> ordersQuery = context.Orders.Include(x => x.Product);

                ordersQuery = ordersQuery.FilterByProduct(filter.ProductFilter);

                ordersQuery = ordersQuery.Where(x => string.IsNullOrWhiteSpace(filter.Notes) || x.Notes.Contains(filter.Notes));

                ordersQuery = ordersQuery.Where(x => filter.PositionsOfInterest == null
                                                    || !filter.PositionsOfInterest.Any()
                                                    || x.PositionsOfInterest.Any(y => filter.PositionsOfInterest.Contains(y)));

                ordersQuery = FilterByQuantity(ordersQuery, filter.Quantity, filter.QuantityComparator);
                ordersQuery = FilterByDateCompleted(ordersQuery, filter.Completed, filter.CompletedComparator);

                orders = await _repositoryBase.TakePage(ordersQuery, filter).ConfigureAwait(false);
            }

            return orders;
        }

        public async Task<Order> Get(Guid itemGuid)
        {
            Order order;

            using (HireRightDbContext context = new HireRightDbContext())
            {
                order = await _repositoryBase.GetBase(itemGuid, context.Orders.Include(x => x.Product)).ConfigureAwait(false);
            }

            return order;
        }

        public async Task<Order> Update(Order itemToUpdate)
        {
            using (HireRightDbContext context = new HireRightDbContext())
            {
                return await _repositoryBase.UpdateBase(itemToUpdate, context.Orders, context).ConfigureAwait(false);
            }
        }

        private IQueryable<Order> FilterByDateCompleted(IQueryable<Order> query, DateTime? value, DateTimeSearchComparators? comparator)
        {
            if (value == null || comparator == null) return query;

            switch (comparator)
            {
                case DateTimeSearchComparators.After:
                    return query.Where(x => x.Completed > value);

                case DateTimeSearchComparators.Before:
                    return query.Where(x => x.Completed > value);

                case DateTimeSearchComparators.DayOf:
                    return query.Where(x => x.Completed >= (value - TimeSpan.FromDays(1)) && x.Completed <= (value + TimeSpan.FromDays(1)));

                case DateTimeSearchComparators.Exactly:
                    return query.Where(x => x.Completed == value);

                case DateTimeSearchComparators.HourOf:
                    return query.Where(x => x.Completed >= (value - TimeSpan.FromHours(1)) && x.Completed <= (value + TimeSpan.FromHours(1)));

                default:
                    return query;
            }
        }

        private IQueryable<Order> FilterByQuantity(IQueryable<Order> query, decimal? value, NumericSearchComparators? comparator)
        {
            if (value == null || comparator == null) return query;

            switch (comparator)
            {
                case NumericSearchComparators.GreaterThan:
                    return query.Where(x => x.Quantity > value.Value);

                case NumericSearchComparators.GreaterThanOrEqualTo:
                    return query.Where(x => x.Quantity >= value.Value);

                case NumericSearchComparators.EqualTo:
                    return query.Where(x => x.Quantity == value.Value);

                case NumericSearchComparators.LessThanOrEqualTo:
                    return query.Where(x => x.Quantity <= value.Value);

                case NumericSearchComparators.LessThan:
                    return query.Where(x => x.Quantity < value.Value);

                default:
                    return query;
            }
        }
    }
}