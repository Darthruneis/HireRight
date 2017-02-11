using DataTransferObjects.Data_Transfer_Objects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataTransferObjects.Filters.Concrete;
using HireRight.EntityFramework.CodeFirst.Models.CompanyAggregate;

namespace HireRight.BusinessLogic.Abstract
{
    public interface IProductsBusinessLogic : IBusinessLogicBase<Product, ProductDTO>
    {
        Task<ProductDTO> Add(ProductDTO productDto);

        Task<ProductDTO> Get(Guid productGuid);

        Task<List<ProductDTO>> Get(ProductFilter filterParameters);

        Task<ProductDTO> Update(ProductDTO productDto);
    }
}