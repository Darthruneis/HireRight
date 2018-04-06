using DataTransferObjects.Filters.Concrete;
using HireRight.EntityFramework.CodeFirst.Database_Context;
using HireRight.EntityFramework.CodeFirst.Models.CompanyAggregate;
using HireRight.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using DataTransferObjects.Data_Transfer_Objects;
using HireRight.EntityFramework.CodeFirst.Models;

namespace HireRight.Repository.Concrete
{

    public class ProductsRepository : RepositoryBase<Product>, IProductsRepository
    {
        public ProductsRepository() : base(() => new HireRightDbContext()) { }
        public ProductsRepository(Func<HireRightDbContext> contextFunc) : base(contextFunc) { }

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
                                           .Select(x => new { x.Id, x.StaticId, x.RowGuid, x.Title, x.Price, Discounts = x.Discounts.Where(y => y.IsActive) })
                                           .FirstOrDefaultAsync();

                if (product == null)
                    return null;

                return new Product(product.Title, product.Price, product.RowGuid, product.Discounts.ToList());
            }
        }

        public async Task<Maybe<Product>> GetWithDiscounts(Guid productGuid)
        {
            using (var context = ContextFunc())
            {
                return await context.Products
                                    .Where(x => x.IsActive)
                                    .Where(x => x.RowGuid == productGuid)
                                    .Include(x => x.Discounts)
                                    .FirstOrDefaultAsync();
            }
        }

        public async Task<Maybe<ProductDTO>> GetDto(Guid productGuid)
        {
            using (HireRightDbContext context = ContextFunc.Invoke())
            {
                return await context.Products
                                           .Where(x => x.IsActive)
                                           .Where(x => x.RowGuid == productGuid)
                                           .Select(x => new ProductDTO { Title = x.Title, RowGuid = x.RowGuid, Price = x.Price })
                                           .FirstOrDefaultAsync();
            }
        }

        public async Task<Maybe<ProductDTO>> GetDtoWithDiscounts(Guid productGuid)
        {
            using (HireRightDbContext context = ContextFunc.Invoke())
            {
                return await context.Products
                                    .Where(x => x.IsActive)
                                    .Where(x => x.RowGuid == productGuid)
                                    .Select(x => new ProductDTO()
                                                 {
                                                     Title = x.Title,
                                                     RowGuid = x.RowGuid,
                                                     Price = x.Price,
                                                     Discounts = x.Discounts.Select(y => new DiscountDTO()
                                                                                         {
                                                                                             Amount = y.Amount,
                                                                                             IsPercent = y.IsPercent,
                                                                                             RowGuid = y.RowGuid,
                                                                                             Threshold = y.Threshold
                                                                                         }).ToList(),
                                                 })
                                    .FirstOrDefaultAsync();
            }
        }

        public async Task<ICollection<DiscountDTO>> GetDiscountsForProduct(Guid productGuid)
        {
            using (HireRightDbContext context = ContextFunc.Invoke())
            {
                return await context.Discounts
                                    .Where(x => x.IsActive)
                                    .Where(x => x.Product.RowGuid == productGuid)
                                    .Select(x => new DiscountDTO() { RowGuid = x.RowGuid, Threshold = x.Threshold, Amount = x.Amount, IsPercent = x.IsPercent })
                                    .ToListAsync();
            }
        }

        public async Task<Product> Update(Product itemToUpdate)
        {
            using (HireRightDbContext context = ContextFunc.Invoke())
            {
                return await UpdateBase(itemToUpdate, context.Products, context).ConfigureAwait(false);
            }
        }
    }
}