using DataTransferObjects.Data_Transfer_Objects;
using SDK.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataTransferObjects;
using DataTransferObjects.Filters;
using DataTransferObjects.Filters.Concrete;

namespace SDK.Concrete
{
    public class ClientsSDK : IClientsSDK
    {
        private readonly IApiSDKClient<ClientDTO> _client;

        public ClientsSDK(IApiSDKClient<ClientDTO> client)
        {
            _client = client;
            _client.BaseAddress = new Uri(ConfigurationConstants.ApiUrl + typeof(ClientsSDK).Name.Replace("SDK", string.Empty));
        }

        public async Task<ClientDTO> AddClient(ClientDTO clientToAdd)
        {
            ApiResponse<ClientDTO> response = await _client.PostAsync(clientToAdd);

            return response.Results.First();
        }

        public async Task<ClientDTO> GetClient(Guid clientGuid)
        {
            ApiResponse<ClientDTO> response = await _client.GetAsync($"?{nameof(clientGuid)}={clientGuid}");

            return response.Results.First();
        }

        public async Task<List<ClientDTO>> GetClients(ClientFilter filter)
        {
            ApiResponse<ClientDTO> response = await _client.GetAsync(filter.CreateQuery());

            return response.Results.ToList();
        }

        public async Task<ClientDTO> UpdateClient(ClientDTO clientToUpdate)
        {
            ApiResponse<ClientDTO> response = await _client.PutAsync(clientToUpdate);

            return response.Results.First();
        }
    }
}