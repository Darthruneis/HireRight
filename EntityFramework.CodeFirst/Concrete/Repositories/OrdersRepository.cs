using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DataTransferObjects.Filters.Concrete;
using HireRight.Persistence.Abstract;
using HireRight.Persistence.Database_Context;
using HireRight.Persistence.Models;
using HireRight.Persistence.Models.CompanyAggregate;

namespace HireRight.Persistence.Concrete.Repositories
{
    public class OrdersRepository : RepositoryBase<Order>, IOrdersRepository
    {
        public OrdersRepository() : base(() => new HireRightDbContext())
        {
        }

        public OrdersRepository(Func<HireRightDbContext> contextFunc) : base(contextFunc)
        {
        }

        public async Task<Order> Add(Order itemToAdd)
        {
            using (HireRightDbContext context = ContextFunc.Invoke())
            {
                return await AddBase(itemToAdd, context.Orders, context).ConfigureAwait(false);
            }
        }

        public async Task<PageResult<Order>> Get(OrderFilter filter)
        {
            using (HireRightDbContext context = ContextFunc.Invoke())
            {
                IQueryable<Order> ordersQuery = context.Orders.Include(x => x.Product);

                ordersQuery = ordersQuery.Where(x => string.IsNullOrWhiteSpace(filter.Notes) || x.Notes.Contains(filter.Notes));

                ordersQuery = ordersQuery.Where(x => filter.PositionsOfInterest == null
                                                    || !filter.PositionsOfInterest.Any()
                                                    || x.PositionsOfInterest.Any(y => filter.PositionsOfInterest.Contains(y)));

                ordersQuery = FilterByQuantity(ordersQuery, filter.Quantity, filter.QuantityComparator);
                ordersQuery = FilterByDateCompleted(ordersQuery, filter.Completed, filter.CompletedComparator);

                PageResult<Order> orders = await TakePage(ordersQuery, filter).ConfigureAwait(false);
                return orders;
            }
        }

        public async Task<Order> Get(Guid itemGuid)
        {
            using (HireRightDbContext context = ContextFunc.Invoke())
            {
                Order order = await GetBase(itemGuid, context.Orders.Include(x => x.Product)).ConfigureAwait(false);
                return order;
            }
        }

        public async Task<Order> Update(Order itemToUpdate)
        {
            using (HireRightDbContext context = ContextFunc.Invoke())
            {
                return await UpdateBase(itemToUpdate, context.Orders, context).ConfigureAwait(false);
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