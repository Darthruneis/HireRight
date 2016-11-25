using HireRight.EntityFramework.CodeFirst.Database_Context;
using HireRight.EntityFramework.CodeFirst.Models;
using HireRight.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DataTransferObjects.Filters;
using DataTransferObjects.Filters.Concrete;

namespace HireRight.Repository.Concrete
{
    public class ClientsRepository : IClientsRepository
    {
        private readonly IRepositoryBase<Client> _repositoryBase;

        public ClientsRepository(IRepositoryBase<Client> repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }

        public async Task<Client> Add(Client itemToAdd)
        {
            using (HireRightDbContext context = new HireRightDbContext())
            {
                return await _repositoryBase.AddBase(itemToAdd, context.Clients).ConfigureAwait(false);
            }
        }

        public async Task<List<Client>> Get(ClientFilter filter)
        {
            List<Client> clients;

            using (HireRightDbContext context = new HireRightDbContext())
            {
                IQueryable<Client> clientsQuery = context.Clients.Include(x => x.Account).Include(x => x.Admin).Include(x => x.PrimaryContact);

                clientsQuery = RepositoryQueryFilterer.FilterClientQuery(clientsQuery, filter);

                clients = await _repositoryBase.TakePage(clientsQuery, filter).ConfigureAwait(false);
            }

            return clients;
        }

        public async Task<Client> Get(Guid itemGuid)
        {
            Client client;

            using (HireRightDbContext context = new HireRightDbContext())
            {
                client = await _repositoryBase.GetBase(itemGuid, context.Clients.Include(x => x.Account).Include(x => x.Admin).Include(x => x.PrimaryContact)).ConfigureAwait(false);
            }

            return client;
        }

        public async Task<Client> Update(Client itemToUpdate)
        {
            using (HireRightDbContext context = new HireRightDbContext())
            {
                return await _repositoryBase.UpdateBase(itemToUpdate, context.Clients).ConfigureAwait(false);
            }
        }
    }
}