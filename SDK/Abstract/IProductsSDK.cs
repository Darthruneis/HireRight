using DataTransferObjects.Data_Transfer_Objects;
using DataTransferObjects.Filters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataTransferObjects.Filters.Concrete;

namespace SDK.Abstract
{
    public interface IProductsSDK
    {
        Task<ProductDTO> AddProduct(ProductDTO productToAdd);

        Task<ProductDTO> GetProduct(Guid productGuid);

        Task<List<ProductDTO>> GetProducts(ProductFilter filterParameters);

        Task<ProductDTO> UpdateProduct(ProductDTO productToUpdate);
    }
}