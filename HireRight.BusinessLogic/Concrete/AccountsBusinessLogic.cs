using DataTransferObjects.Data_Transfer_Objects;
using DataTransferObjects.Filters;
using HireRight.BusinessLogic.Abstract;
using HireRight.EntityFramework.CodeFirst.Models;
using HireRight.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataTransferObjects.Filters.Concrete;

namespace HireRight.BusinessLogic.Concrete
{
    public class AccountsBusinessLogic : IAccountsBusinessLogic
    {
        private readonly IAccountsRepository _accountsRepository;

        public AccountsBusinessLogic(IAccountsRepository repo)
        {
            _accountsRepository = repo;
        }

        public async Task<AccountDTO> Add(AccountDTO accountDto)
        {
            Account accountToAdd = ConvertDtoToModel(accountDto);

            return ConvertModelToDto(await _accountsRepository.Add(accountToAdd).ConfigureAwait(false));
        }

        public Account ConvertDtoToModel(AccountDTO dto)
        {
            Account model = new Account();
            model.Id = dto.Id;
            model.CreatedUtc = dto.CreatedUtc;

            return model;
        }

        public AccountDTO ConvertModelToDto(Account model)
        {
            AccountDTO dto = new AccountDTO();
            dto.Id = model.Id;
            dto.CreatedUtc = model.CreatedUtc;

            return dto;
        }

        public async Task<AccountDTO> Get(Guid accountGuid)
        {
            Account account = await _accountsRepository.Get(accountGuid).ConfigureAwait(false);

            return ConvertModelToDto(account);
        }

        public async Task<List<AccountDTO>> Get(AccountFilter filter)
        {
            List<Account> accounts = await _accountsRepository.Get(filter).ConfigureAwait(false);

            return accounts.Select(ConvertModelToDto).ToList();
        }

        public async Task<AccountDTO> Update(AccountDTO accountDto)
        {
            Account accountToUpdate = ConvertDtoToModel(accountDto);

            return ConvertModelToDto(await _accountsRepository.Update(accountToUpdate).ConfigureAwait(false));
        }
    }
}