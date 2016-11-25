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
    public class AccountsSDK : IAccountsSDK
    {
        private readonly IApiSDKClient<AccountDTO> _client;

        public AccountsSDK(IApiSDKClient<AccountDTO> client)
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
            ApiResponse<AccountDTO> response = await _client.GetAsync($"?{nameof(accountGuid)}={accountGuid}");

            return response.Results.First();
        }

        public async Task<List<AccountDTO>> GetAccounts(AccountFilter filter)
        {
            ApiResponse<AccountDTO> response = await _client.GetAsync(filter.CreateQuery());

            return response.Results.ToList();
        }

        public async Task<AccountDTO> UpdateAccount(AccountDTO accountToUpdate)
        {
            ApiResponse<AccountDTO> response = await _client.PutAsync(accountToUpdate);

            return response.Results.First();
        }
    }
}