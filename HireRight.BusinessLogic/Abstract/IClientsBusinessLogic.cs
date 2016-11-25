using DataTransferObjects.Data_Transfer_Objects;
using DataTransferObjects.Filters;
using HireRight.EntityFramework.CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataTransferObjects.Filters.Concrete;

namespace HireRight.BusinessLogic.Abstract
{
    public interface IClientsBusinessLogic : IBusinessLogicBase<Client, ClientDTO>
    {
        Task<ClientDTO> Add(ClientDTO clientDto);

        Task<ClientDTO> Get(Guid clientGuid);

        Task<List<ClientDTO>> Get(ClientFilter filter);

        Task<ClientDTO> Update(ClientDTO clientDto);
    }
}