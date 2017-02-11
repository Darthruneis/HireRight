using DataTransferObjects.Data_Transfer_Objects;
using DataTransferObjects.Filters.Concrete;
using HireRight.API.Models;
using HireRight.BusinessLogic.Abstract;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace HireRight.API.Controllers
{
    [RoutePrefix("api/Contacts")]
    public class ContactsController : ApiControllerBase<ContactDTO>
    {
        private readonly IContactsBusinessLogic _contactsBusinessLogic;

        private readonly Func<ContactFilter, Task<List<ContactDTO>>> _getPage;

        public ContactsController(IContactsBusinessLogic bll)
        {
            _contactsBusinessLogic = bll;

            _add = _contactsBusinessLogic.Add;
            _update = _contactsBusinessLogic.Update;
            _getSingle = _contactsBusinessLogic.Get;
            _getPage = _contactsBusinessLogic.Get;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> AddContact(ContactDTO contactToAdd)
        {
            return await AddBase(contactToAdd);
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetContact(Guid contactGuid)
        {
            return await GetSingleBase(contactGuid);
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetContacts([FromUri] ContactFilter filter)
        {
            return await GetMultipleBase(_getPage(filter));
        }

        [Route("ContactConsultant")]
        [HttpPost]
        public async Task<HttpResponseMessage> SendContactConsultantEmail(SendContactConsultantEmailModel model)
        {
            try
            {
                await _contactsBusinessLogic.SendContactConsultantEmail(model.ClientEmail, model.Message);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("NewContact")]
        [HttpPost]
        public async Task<HttpResponseMessage> SendNewContactEmail(SendNewContactEmailModel model)
        {
            try
            {
                await _contactsBusinessLogic.SendNewContactEmail(model.Id, model.Message);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPut]
        public async Task<HttpResponseMessage> UpdateContact(ContactDTO contactToUpdate)
        {
            return await UpdateBase(contactToUpdate);
        }
    }
}