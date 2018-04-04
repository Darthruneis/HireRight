﻿using DataTransferObjects.Filters.Concrete;
using HireRight.EntityFramework.CodeFirst.Database_Context;
using HireRight.EntityFramework.CodeFirst.Models.CompanyAggregate;
using HireRight.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using HireRight.EntityFramework.CodeFirst.Models;

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

        public async Task<PageResult<Product>> Get(ProductFilter filter)
        {
            try
            {
                using (HireRightDbContext context = ContextFunc.Invoke())
                {
                    var productsQuery = context.Products.Where(x => x.IsActive)
                                               .Select(x => new { x.Id, x.Title, Discounts = x.Discounts.Where(y => y.IsActive), x.RowGuid, x.Price, x.StaticId });

                    if (!string.IsNullOrWhiteSpace(filter.Title))
                        productsQuery = productsQuery.Where(x => x.Title.Contains(filter.Title));

                    //productsQuery = FilterByPrice(productsQuery, filter.Price, filter.PriceComparator);
                    var products = await TakePage(productsQuery, filter, x => x.Id).ConfigureAwait(false);
                    return PageResult<Product>.Ok(products.TotalMatchingResults, products.Results.Select(x => new Product() { Discounts = x.Discounts.ToList(), Id = x.Id, Title = x.Title, StaticId = x.StaticId, RowGuid = x.RowGuid, Price = x.Price }).ToList(), products.PageNumber, products.PageSize);
                }
            }
            catch (Exception ex)
            {
                throw new AggregateException("Database issue encountered.", ex);
            }
        }

        public async Task<Product> Get(Guid itemGuid)
        {
            using (HireRightDbContext context = ContextFunc.Invoke())
            {
                var product = await context.Products
                                           .Where(x => x.IsActive)
                                           .Where(x => x.RowGuid == itemGuid)
                                           .Select(x => new {x.Id, x.StaticId, x.RowGuid, x.Title, x.Price, Discounts = x.Discounts.Where(y => y.IsActive)})
                                           .FirstOrDefaultAsync();

                if (product == null)
                    return null;

                return new Product(product.Title, product.Price, product.RowGuid, product.Discounts.ToList());
            }
        }

        public async Task<ICollection<Discount>> GetDiscountsForProduct(Guid productGuid)
        {
            using (HireRightDbContext context = ContextFunc.Invoke())
            {
                return await context.Discounts.Where(x => x.IsActive).Where(x => x.Product.RowGuid == productGuid).ToListAsync();
            }
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