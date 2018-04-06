using DataTransferObjects.Data_Transfer_Objects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataTransferObjects.Filters.Concrete;
using HireRight.Persistence;
using HireRight.Persistence.Models.CompanyAggregate;

namespace HireRight.BusinessLogic.Abstract
{
    public interface IProductsBusinessLogic : IBusinessLogicBase<Product, ProductDTO>
    {
        Task<ProductDTO> Add(ProductDTO productDto);

        Task<PagingResultDTO<ProductDTO>> Get(ProductFilter filterParameters);

        Task<ProductDTO> Update(ProductDTO productDto);

        Task<ICollection<DiscountDTO>> GetDiscountsForPoduct(Guid productGuid);

        Task<Maybe<ProductDTO>> GetDto(Guid productGuid);

        Task<Maybe<ProductDTO>> GetDtoWithDiscounts(Guid productGuid);
    }
}