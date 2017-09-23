using DataTransferObjects.Data_Transfer_Objects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataTransferObjects.Filters.Concrete;
using HireRight.EntityFramework.CodeFirst.Models.CompanyAggregate;

namespace HireRight.BusinessLogic.Abstract
{
    public interface IContactsBusinessLogic : IBusinessLogicBase<Contact, ContactDTO>
    {
        Task<ContactDTO> Add(ContactDTO contactDto);

        Task<ContactDTO> Get(Guid contactGuid);

        Task<PagingResultDTO<ContactDTO>> Get(ContactFilter filter);

        void SendContactConsultantEmail(string clientEmailAddress, string message);

        Task SendNewContactEmail(Guid contactId, string message);

        Task<ContactDTO> Update(ContactDTO contactDto);
    }
}