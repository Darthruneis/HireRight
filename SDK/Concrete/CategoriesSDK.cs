using DataTransferObjects;
using DataTransferObjects.Data_Transfer_Objects;
using SDK.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataTransferObjects.Filters.Concrete;

namespace SDK.Concrete
{
    public class CategoriesSDK : ICategoriesSDK
    {
        private readonly IApiSDKClient _client;

        public CategoriesSDK(IApiSDKClient client)
        {
            _client = client;
            client.BaseAddress = new Uri(ConfigurationConstants.ApiUrl + typeof(CategoriesSDK).Name.Replace("SDK", string.Empty));
        }

        public async Task<CategoryDTO> AddCategory(CategoryDTO categoryToAdd)
        {
            ApiResponse<CategoryDTO> response = await _client.PostAsync(categoryToAdd);

            return response.Results.First();
        }

        public async Task<List<CategoryDTO>> GetCategories(CategoryFilter filter)
        {
            ApiResponse<CategoryDTO> response = await _client.GetAsync<CategoryDTO>(filter.CreateQuery());

            return response.Results.ToList();
        }

        public async Task<CategoryDTO> GetCategory(Guid categoryGuid)
        {
            ApiResponse<CategoryDTO> response = await _client.GetAsync<CategoryDTO>($"?{nameof(categoryGuid)}={categoryGuid}");

            return response.Results.First();
        }

        public async Task<CategoryDTO> UpdateCategory(CategoryDTO categoryToUpdate)
        {
            ApiResponse<CategoryDTO> response = await _client.PutAsync(categoryToUpdate);

            return response.Results.First();
        }
    }
}