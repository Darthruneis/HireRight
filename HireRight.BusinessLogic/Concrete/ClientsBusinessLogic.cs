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
    public class ClientsBusinessLogic : IClientsBusinessLogic
    {
        private readonly IClientsRepository _clientsRepository;

        public ClientsBusinessLogic(IClientsRepository repo)
        {
            _clientsRepository = repo;
        }

        public async Task<ClientDTO> Add(ClientDTO clientDto)
        {
            Client clientToAdd = ConvertDtoToModel(clientDto);

            return ConvertModelToDto(await _clientsRepository.Add(clientToAdd).ConfigureAwait(false));
        }

        public Client ConvertDtoToModel(ClientDTO dto)
        {
            Client model = new Client();
            model.Id = dto.Id;
            model.CreatedUtc = dto.CreatedUtc;
            model.PrimaryContactId = model.PrimaryContactId;
            model.AdminContactId = model.AdminContactId;

            return model;
        }

        public ClientDTO ConvertModelToDto(Client model)
        {
            ClientDTO dto = new ClientDTO();
            dto.Id = model.Id;
            dto.CreatedUtc = model.CreatedUtc;

            return dto;
        }

        public async Task<ClientDTO> Get(Guid clientGuid)
        {
            Client client = await _clientsRepository.Get(clientGuid).ConfigureAwait(false);

            return ConvertModelToDto(client);
        }

        public async Task<List<ClientDTO>> Get(ClientFilter filter)
        {
            List<Client> clients = await _clientsRepository.Get(filter).ConfigureAwait(false);

            return clients.Select(ConvertModelToDto).ToList();
        }

        public async Task<ClientDTO> Update(ClientDTO clientDto)
        {
            Client clientToUpdate = ConvertDtoToModel(clientDto);

            return ConvertModelToDto(await _clientsRepository.Update(clientToUpdate).ConfigureAwait(false));
        }
    }
}