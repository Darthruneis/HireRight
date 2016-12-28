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
    public class LocationsSDK : ILocationsSDK
    {
        private readonly IApiSDKClient _client;

        public LocationsSDK(IApiSDKClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri(ConfigurationConstants.ApiUrl + typeof(LocationsSDK).Name.Replace("SDK", string.Empty));
        }

        public async Task<LocationDTO> AddLocation(LocationDTO locationToAdd)
        {
            ApiResponse<LocationDTO> response = await _client.PostAsync(locationToAdd);

            return response.Results.First();
        }

        public async Task<LocationDTO> GetLocation(Guid locationGuid)
        {
            ApiResponse<LocationDTO> response = await _client.GetAsync<LocationDTO>($"?{nameof(locationGuid)}={locationGuid}");

            return response.Results.First();
        }

        public async Task<List<LocationDTO>> GetLocations(LocationFilter filter)
        {
            ApiResponse<LocationDTO> response = await _client.GetAsync<LocationDTO>(filter.CreateQuery());

            return response.Results.ToList();
        }

        public async Task<LocationDTO> UpdateLocation(LocationDTO locationToUpdate)
        {
            ApiResponse<LocationDTO> response = await _client.PutAsync(locationToUpdate);

            return response.Results.First();
        }
    }
}