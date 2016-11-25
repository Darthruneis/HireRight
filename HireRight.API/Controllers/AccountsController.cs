using DataTransferObjects.Data_Transfer_Objects;
using DataTransferObjects.Filters;
using HireRight.BusinessLogic.Abstract;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DataTransferObjects.Filters.Concrete;

namespace HireRight.API.Controllers
{
    [RoutePrefix("api/Accounts")]
    public class AccountsController : ApiControllerBase<AccountDTO>
    {
        private readonly IAccountsBusinessLogic _accountsBusinessLogic;

        private readonly Func<AccountFilter, Task<List<AccountDTO>>> _getPage;

        public AccountsController(IAccountsBusinessLogic bll)
        {
            _accountsBusinessLogic = bll;

            _add = _accountsBusinessLogic.Add;
            _update = _accountsBusinessLogic.Update;
            _getSingle = _accountsBusinessLogic.Get;
            _getPage = _accountsBusinessLogic.Get;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> AddAccount(AccountDTO accountToAdd)
        {
            return await AddBase(accountToAdd);
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetAccount(Guid accountGuid)
        {
            return await GetSingleBase(accountGuid);
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetAccounts(AccountFilter filter)
        {
            return await GetMultipleBase(_getPage(filter));
        }

        [HttpPut]
        public async Task<HttpResponseMessage> UpdateAccount(AccountDTO accountToUpdate)
        {
            return await UpdateBase(accountToUpdate);
        }
    }
}