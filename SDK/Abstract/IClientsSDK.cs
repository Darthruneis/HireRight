using DataTransferObjects.Data_Transfer_Objects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataTransferObjects.Filters;
using DataTransferObjects.Filters.Concrete;

namespace SDK.Abstract
{
    public interface IClientsSDK
    {
        Task<ClientDTO> AddClient(ClientDTO clientToAdd);

        Task<ClientDTO> GetClient(Guid clientGuid);

        Task<List<ClientDTO>> GetClients(ClientFilter filter);

        Task<ClientDTO> UpdateClient(ClientDTO clientToUpdate);
    }
}