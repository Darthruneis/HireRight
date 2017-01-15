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
    public class DiscountsRepository : IDiscountsRepository
    {
        private readonly IRepositoryBase<Discount> _repositoryBase;

        public DiscountsRepository(IRepositoryBase<Discount> repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }

        public async Task<Discount> Add(Discount itemToAdd)
        {
            using (HireRightDbContext context = new HireRightDbContext())
            {
                return await _repositoryBase.AddBase(itemToAdd, context.Discounts, context).ConfigureAwait(false);
            }
        }

        public async Task<List<Discount>> Get(DiscountFilter filter)
        {
            List<Discount> discounts;

            using (HireRightDbContext context = new HireRightDbContext())
            {
                IQueryable<Discount> discountsQuery = context.Discounts;

                discountsQuery = FilterByAmount(discountsQuery, filter.Amount, filter.AmountComparator);

                discountsQuery = FilterByThreshold(discountsQuery, filter.Threshold, filter.ThresholdComparator);

                discountsQuery = discountsQuery.Where(x => filter.IsPercent == null || x.IsPercent == filter.IsPercent.Value);

                discounts = await _repositoryBase.TakePage(discountsQuery, filter).ConfigureAwait(false);
            }

            return discounts;
        }

        public async Task<Discount> Get(Guid itemGuid)
        {
            Discount discount;

            using (HireRightDbContext context = new HireRightDbContext())
            {
                discount = await _repositoryBase.GetBase(itemGuid, context.Discounts).ConfigureAwait(false);
            }

            return discount;
        }

        public async Task<Discount> Update(Discount itemToUpdate)
        {
            using (HireRightDbContext context = new HireRightDbContext())
            {
                return await _repositoryBase.UpdateBase(itemToUpdate, context.Discounts, context).ConfigureAwait(false);
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