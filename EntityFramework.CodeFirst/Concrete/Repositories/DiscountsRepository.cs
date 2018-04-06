using System;
using System.Linq;
using System.Threading.Tasks;
using DataTransferObjects.Filters.Concrete;
using HireRight.Persistence.Abstract;
using HireRight.Persistence.Database_Context;
using HireRight.Persistence.Models;
using HireRight.Persistence.Models.CompanyAggregate;

namespace HireRight.Persistence.Concrete.Repositories
{
    public class DiscountsRepository : RepositoryBase<Discount>, IDiscountsRepository
    {
        public DiscountsRepository() : base(() => new HireRightDbContext())
        {
        }

        public DiscountsRepository(Func<HireRightDbContext> contextFunc) : base(contextFunc)
        {
        }

        public async Task<Discount> Add(Discount itemToAdd)
        {
            using (HireRightDbContext context = ContextFunc.Invoke())
            {
                return await AddBase(itemToAdd, context.Discounts, context).ConfigureAwait(false);
            }
        }

        public async Task<PageResult<Discount>> Get(DiscountFilter filter)
        {
            using (HireRightDbContext context = ContextFunc.Invoke())
            {
                IQueryable<Discount> discountsQuery = context.Discounts.Where(x => x.IsActive);

                discountsQuery = FilterByAmount(discountsQuery, filter.Amount, filter.AmountComparator);

                discountsQuery = FilterByThreshold(discountsQuery, filter.Threshold, filter.ThresholdComparator);

                discountsQuery = discountsQuery.Where(x => filter.IsPercent == null || x.IsPercent == filter.IsPercent.Value);

                PageResult<Discount> discounts = await TakePage(discountsQuery, filter).ConfigureAwait(false);
                return discounts;
            }
        }

        public async Task<Discount> Get(Guid itemGuid)
        {
            using (HireRightDbContext context = ContextFunc.Invoke())
            {
                Discount discount = await GetBase(itemGuid, context.Discounts).ConfigureAwait(false);
                return discount;
            }
        }

        public async Task<Discount> Update(Discount itemToUpdate)
        {
            using (HireRightDbContext context = ContextFunc.Invoke())
            {
                return await UpdateBase(itemToUpdate, context.Discounts, context).ConfigureAwait(false);
            }
        }

        private IQueryable<Discount> FilterByAmount(IQueryable<Discount> query, decimal? value, NumericSearchComparators? comparator)
        {
            if (value == null || comparator == null) return query;

            switch (comparator)
            {
                case NumericSearchComparators.GreaterThan:
                    return query.Where(x => x.Threshold > value.Value);

                case NumericSearchComparators.GreaterThanOrEqualTo:
                    return query.Where(x => x.Threshold >= value.Value);

                case NumericSearchComparators.EqualTo:
                    return query.Where(x => x.Threshold == value.Value);

                case NumericSearchComparators.LessThanOrEqualTo:
                    return query.Where(x => x.Threshold <= value.Value);

                case NumericSearchComparators.LessThan:
                    return query.Where(x => x.Threshold < value.Value);

                default:
                    return query;
            }
        }

        private IQueryable<Discount> FilterByThreshold(IQueryable<Discount> query, int? value, NumericSearchComparators? comparator)
        {
            if (value == null || comparator == null) return query;

            switch (comparator)
            {
                case NumericSearchComparators.GreaterThan:
                    return query.Where(x => x.Amount > value.Value);

                case NumericSearchComparators.GreaterThanOrEqualTo:
                    return query.Where(x => x.Amount >= value.Value);

                case NumericSearchComparators.EqualTo:
                    return query.Where(x => x.Amount == value.Value);

                case NumericSearchComparators.LessThanOrEqualTo:
                    return query.Where(x => x.Amount <= value.Value);

                case NumericSearchComparators.LessThan:
                    return query.Where(x => x.Amount < value.Value);

                default:
                    return query;
            }
        }
    }
}