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
    [RoutePrefix("api/Clients")]
    public class ClientsController : ApiControllerBase<ClientDTO>
    {
        private readonly IClientsBusinessLogic _clientsBusinessLogic;

        private readonly Func<ClientFilter, Task<List<ClientDTO>>> _getPage;

        public ClientsController(IClientsBusinessLogic bll)
        {
            _clientsBusinessLogic = bll;

            _add = _clientsBusinessLogic.Add;
            _update = _clientsBusinessLogic.Update;
            _getSingle = _clientsBusinessLogic.Get;
            _getPage = _clientsBusinessLogic.Get;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> AddClient(ClientDTO clientToAdd)
        {
            return await AddBase(clientToAdd);
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetClient(Guid clientGuid)
        {
            return await GetSingleBase(clientGuid);
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetClients(ClientFilter filter)
        {
            return await GetMultipleBase(_getPage(filter));
        }

        [HttpPut]
        public async Task<HttpResponseMessage> UpdateClient(ClientDTO clientToUpdate)
        {
            return await UpdateBase(clientToUpdate);
        }
    }
}