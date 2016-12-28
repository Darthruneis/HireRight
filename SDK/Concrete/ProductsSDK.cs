using DataTransferObjects;
using DataTransferObjects.Data_Transfer_Objects;
using DataTransferObjects.Filters;
using DataTransferObjects.Filters.Concrete;
using SDK.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDK.Concrete
{
    public class ProductsSDK : IProductsSDK
    {
        private readonly IApiSDKClient _client;

        public ProductsSDK(IApiSDKClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri(ConfigurationConstants.ApiUrl + typeof(ProductsSDK).Name.Replace("SDK", string.Empty));
        }

        public async Task<ProductDTO> AddProduct(ProductDTO productToAdd)
        {
            ApiResponse<ProductDTO> response = await _client.PostAsync(productToAdd);

            return response.Results.First();
        }

        public async Task<ProductDTO> GetProduct(Guid productGuid)
        {
            ApiResponse<ProductDTO> response = await _client.GetAsync<ProductDTO>($"?{nameof(productGuid)}={productGuid}");

            return response.Results.First();
        }

        public async Task<List<ProductDTO>> GetProducts(ProductFilter filterParameters)
        {
            ApiResponse<ProductDTO> response = await _client.GetAsync<ProductDTO>(filterParameters.CreateQuery());

            return response.Results.ToList();
        }

        public async Task<ProductDTO> UpdateProduct(ProductDTO productToUpdate)
        {
            ApiResponse<ProductDTO> response = await _client.PutAsync(productToUpdate);

            return response.Results.First();
        }
    }
}