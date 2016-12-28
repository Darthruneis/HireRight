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
    public class AccountsSDK : IAccountsSDK
    {
        private readonly IApiSDKClient _client;

        public AccountsSDK(IApiSDKClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri(ConfigurationConstants.ApiUrl + typeof(AccountsSDK).Name.Replace("SDK", string.Empty));
        }

        public async Task<AccountDTO> AddAccount(AccountDTO accountToAdd)
        {
            ApiResponse<AccountDTO> response = await _client.PostAsync(accountToAdd);

            return response.Results.First();
        }

        public async Task<AccountDTO> GetAccount(Guid accountGuid)
        {
            ApiResponse<AccountDTO> response = await _client.GetAsync<AccountDTO>($"?{nameof(accountGuid)}={accountGuid}");

            return response.Results.First();
        }

        public async Task<List<AccountDTO>> GetAccounts(AccountFilter filter)
        {
            ApiResponse<AccountDTO> response = await _client.GetAsync<AccountDTO>(filter.CreateQuery());

            return response.Results.ToList();
        }

        public async Task<AccountDTO> UpdateAccount(AccountDTO accountToUpdate)
        {
            ApiResponse<AccountDTO> response = await _client.PutAsync(accountToUpdate);

            return response.Results.First();
        }
    }
}