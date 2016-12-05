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

namespace HireRight.Repository.Concrete
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly IRepositoryBase<Product> _repositoryBase;

        public ProductsRepository(IRepositoryBase<Product> repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }

        public async Task<Product> Add(Product itemToAdd)
        {
            using (HireRightDbContext context = new HireRightDbContext())
            {
                return await _repositoryBase.AddBase(itemToAdd, context.Products).ConfigureAwait(false);
            }
        }

        public async Task<List<Product>> Get(ProductFilter filter)
        {
            List<Product> products;

            using (HireRightDbContext context = new HireRightDbContext())
            {
                IQueryable<Product> productsQuery = context.Products.Include(x => x.Discounts);

                productsQuery = productsQuery.FilterByDiscounts(filter.DiscountFilter);

                products = await _repositoryBase.TakePage(productsQuery, filter).ConfigureAwait(false);
            }

            return products;
        }

        public async Task<Product> Get(Guid itemGuid)
        {
            Product product;

            using (HireRightDbContext context = new HireRightDbContext())
            {
                product = await _repositoryBase.GetBase(itemGuid, context.Products.Include(x => x.Discounts)).ConfigureAwait(false);
            }

            return product;
        }

        public async Task<Product> Update(Product itemToUpdate)
        {
            using (HireRightDbContext context = new HireRightDbContext())
            {
                return await _repositoryBase.UpdateBase(itemToUpdate, context.Products).ConfigureAwait(false);
            }
        }
    }
}