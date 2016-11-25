using HireRight.EntityFramework.CodeFirst.Database_Context;
using HireRight.EntityFramework.CodeFirst.Models;
using HireRight.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DataTransferObjects.Filters;
using DataTransferObjects.Filters.Concrete;

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
                return await _repositoryBase.AddBase(itemToAdd, context.Discounts).ConfigureAwait(false);
            }
        }

        public async Task<List<Discount>> Get(DiscountFilter filter)
        {
            List<Discount> discounts;

            using (HireRightDbContext context = new HireRightDbContext())
            {
                IQueryable<Discount> discountsQuery = context.Discounts.Include(x => x.Product);

                discountsQuery = RepositoryQueryFilterer.FilterDiscountQuery(filter, discountsQuery);

                discounts = await _repositoryBase.TakePage(discountsQuery, filter).ConfigureAwait(false);
            }

            return discounts;
        }

        public async Task<Discount> Get(Guid itemGuid)
        {
            Discount discount;

            using (HireRightDbContext context = new HireRightDbContext())
            {
                discount = await _repositoryBase.GetBase(itemGuid, context.Discounts.Include(x => x.Product)).ConfigureAwait(false);
            }

            return discount;
        }

        public async Task<Discount> Update(Discount itemToUpdate)
        {
            using (HireRightDbContext context = new HireRightDbContext())
            {
                return await _repositoryBase.UpdateBase(itemToUpdate, context.Discounts).ConfigureAwait(false);
            }
        }
    }
}