using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataTransferObjects.Data_Transfer_Objects;
using DataTransferObjects.Filters.Concrete;
using HireRight.Persistence.Models;
using HireRight.Persistence.Models.CompanyAggregate;

namespace HireRight.Persistence.Abstract
{
    public interface IProductsRepository
    {
        Task<Product> Add(Product itemToAdd);

        Task<PageResult<Product>> Get(ProductFilter filter);

        Task<Product> Get(Guid itemGuid);

        Task<Maybe<Product>> GetWithDiscounts(Guid productGuid);

        Task<Product> Update(Product itemToUpdate);

        Task<ICollection<DiscountDTO>> GetDiscountsForProduct(Guid productGuid);

        Task<Maybe<ProductDTO>> GetDto(Guid productGuid);

        Task<Maybe<ProductDTO>> GetDtoWithDiscounts(Guid productGuid);
    }
}