using DataTransferObjects;
using DataTransferObjects.Data_Transfer_Objects;
using SDK.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDK.Concrete
{
    public class CategoriesSDK : ICategoriesSDK
    {
        private readonly IApiSDKClient<CategoryDTO> _client;

        public CategoriesSDK(IApiSDKClient<CategoryDTO> client)
        {
            _client = client;
            client.BaseAddress = new Uri(ConfigurationConstants.ApiUrl + typeof(CategoriesSDK).Name.Replace("SDK", string.Empty));
        }

        public async Task<CategoryDTO> AddCategory(CategoryDTO categoryToAdd)
        {
            ApiResponse<CategoryDTO> response = await _client.PostAsync(categoryToAdd);

            return response.Results.First();
        }

        public async Task<List<CategoryDTO>> GetCategories()
        {
            ApiResponse<CategoryDTO> response = await _client.GetAsync(string.Empty);

            return response.Results.ToList();
        }

        public async Task<CategoryDTO> GetCategory(Guid categoryGuid)
        {
            var response = await _client.GetAsync($"?{nameof(categoryGuid)}={categoryGuid}");

            return response.Results.First();
        }

        public async Task<CategoryDTO> UpdateCategory(CategoryDTO categoryToUpdate)
        {
            ApiResponse<CategoryDTO> response = await _client.PutAsync(categoryToUpdate);

            return response.Results.First();
        }
    }
}