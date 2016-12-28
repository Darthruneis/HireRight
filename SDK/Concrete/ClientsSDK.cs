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
    public class ClientsSDK : IClientsSDK
    {
        private readonly IApiSDKClient _client;

        public ClientsSDK(IApiSDKClient client)
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
            ApiResponse<ClientDTO> response = await _client.GetAsync<ClientDTO>($"?{nameof(clientGuid)}={clientGuid}");

            return response.Results.First();
        }

        public async Task<List<ClientDTO>> GetClients(ClientFilter filter)
        {
            ApiResponse<ClientDTO> response = await _client.GetAsync<ClientDTO>(filter.CreateQuery());

            return response.Results.ToList();
        }

        public async Task<ClientDTO> UpdateClient(ClientDTO clientToUpdate)
        {
            ApiResponse<ClientDTO> response = await _client.PutAsync(clientToUpdate);

            return response.Results.First();
        }
    }
}