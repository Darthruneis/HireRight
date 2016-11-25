using DataTransferObjects.Data_Transfer_Objects;
using DataTransferObjects.Filters;
using HireRight.EntityFramework.CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataTransferObjects.Filters.Concrete;

namespace HireRight.BusinessLogic.Abstract
{
    public interface IAccountsBusinessLogic : IBusinessLogicBase<Account, AccountDTO>
    {
        Task<AccountDTO> Add(AccountDTO accountDto);

        Task<AccountDTO> Get(Guid accountGuid);

        Task<List<AccountDTO>> Get(AccountFilter filter);

        Task<AccountDTO> Update(AccountDTO accountDto);
    }
}