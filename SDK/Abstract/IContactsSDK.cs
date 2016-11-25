using DataTransferObjects.Data_Transfer_Objects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataTransferObjects.Filters;
using DataTransferObjects.Filters.Concrete;

namespace SDK.Abstract
{
    public interface IContactsSDK
    {
        Task<ContactDTO> AddContact(ContactDTO contactToAdd);

        Task<ContactDTO> GetContact(Guid contactGuid);

        Task<List<ContactDTO>> GetContacts(ContactFilter filter);

        Task SendContactConsultantEmail(string clientEmail, string message);

        Task SendNewContactEmail(Guid contactId, string message);

        Task<ContactDTO> UpdateContact(ContactDTO contactToUpdate);
    }
}