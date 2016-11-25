using DataTransferObjects;
using DataTransferObjects.Data_Transfer_Objects;
using DataTransferObjects.Filters;
using SDK.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataTransferObjects.Filters.Concrete;

namespace SDK.Concrete
{
    public class LocationsSDK : ILocationsSDK
    {
        private readonly IApiSDKClient<LocationDTO> _client;

        public LocationsSDK(IApiSDKClient<LocationDTO> client)
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
            ApiResponse<LocationDTO> response = await _client.GetAsync($"?{nameof(locationGuid)}={locationGuid}");

            return response.Results.First();
        }

        public async Task<List<LocationDTO>> GetLocations(LocationFilter filter)
        {
            ApiResponse<LocationDTO> response = await _client.GetAsync(filter.CreateQuery());

            return response.Results.ToList();
        }

        public async Task<LocationDTO> UpdateLocation(LocationDTO locationToUpdate)
        {
            ApiResponse<LocationDTO> response = await _client.PutAsync(locationToUpdate);

            return response.Results.First();
        }
    }
}