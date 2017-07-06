using DataTransferObjects.Filters.Concrete;
using HireRight.EntityFramework.CodeFirst.Database_Context;
using HireRight.EntityFramework.CodeFirst.Models.CompanyAggregate;
using HireRight.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace HireRight.Repository.Concrete
{
    public class ProductsRepository : RepositoryBase<Product>, IProductsRepository
    {
        public ProductsRepository() : base(() => new HireRightDbContext())
        {
        }

        public ProductsRepository(Func<HireRightDbContext> contextFunc) : base(contextFunc)
        {
        }

        public async Task<Product> Add(Product itemToAdd)
        {
            using (HireRightDbContext context = ContextFunc.Invoke())
            {
                return await AddBase(itemToAdd, context.Products, context).ConfigureAwait(false);
            }
        }

        public async Task<List<Product>> Get(ProductFilter filter)
        {
            try
            {
                using (HireRightDbContext context = ContextFunc.Invoke())
                {
                    IQueryable<Product> productsQuery = context.Products.Include(x => x.Discounts);

                    if (!string.IsNullOrWhiteSpace(filter.Title))
                        productsQuery = productsQuery.Where(x => x.Title.Contains(filter.Title));

                    productsQuery = FilterByPrice(productsQuery, filter.Price, filter.PriceComparator);

                    List<Product> products = await TakePage(productsQuery, filter).ConfigureAwait(false);
                    return products;
                }
            }
            catch (Exception ex)
            {
                throw new AggregateException("Database issue encountered.", ex);
            }
        }

        public async Task<Product> Get(Guid itemGuid)
        {
            Product product;

            using (HireRightDbContext context = ContextFunc.Invoke())
            {
                product = await GetBase(itemGuid, context.Products.Include(x => x.Discounts)).ConfigureAwait(false);
            }

            return product;
        }

        public async Task<Product> Update(Product itemToUpdate)
        {
            using (HireRightDbContext context = ContextFunc.Invoke())
            {
                return await UpdateBase(itemToUpdate, context.Products, context).ConfigureAwait(false);
            }
        }

        private IQueryable<Product> FilterByPrice(IQueryable<Product> query, decimal? value, NumericSearchComparators? comparator)
        {
            if (value == null || comparator == null) return query;

            switch (comparator)
            {
                case NumericSearchComparators.GreaterThan:
                    return query.Where(x => x.Price > value.Value);

                case NumericSearchComparators.GreaterThanOrEqualTo:
                    return query.Where(x => x.Price >= value.Value);

                case NumericSearchComparators.EqualTo:
                    return query.Where(x => x.Price == value.Value);

                case NumericSearchComparators.LessThanOrEqualTo:
                    return query.Where(x => x.Price <= value.Value);

                case NumericSearchComparators.LessThan:
                    return query.Where(x => x.Price < value.Value);

                default:
                    return query;
            }
        }
    }
}