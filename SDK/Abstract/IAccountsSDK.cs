using DataTransferObjects.Data_Transfer_Objects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataTransferObjects.Filters;
using DataTransferObjects.Filters.Concrete;

namespace SDK.Abstract
{
    public interface IAccountsSDK
    {
        Task<AccountDTO> AddAccount(AccountDTO accountToAdd);

        Task<AccountDTO> GetAccount(Guid accountGuid);

        Task<List<AccountDTO>> GetAccounts(AccountFilter filter);

        Task<AccountDTO> UpdateAccount(AccountDTO accountToUpdate);
    }
}