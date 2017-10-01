using DataTransferObjects.Data_Transfer_Objects;
using HireRight.BusinessLogic.Abstract;
using HireRight.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataTransferObjects.Filters.Concrete;
using HireRight.BusinessLogic.Extensions;
using HireRight.EntityFramework.CodeFirst.Models;
using HireRight.EntityFramework.CodeFirst.Models.CompanyAggregate;

namespace HireRight.BusinessLogic.Concrete
{
    public class ContactsBusinessLogic : IContactsBusinessLogic
    {
        private readonly IContactsRepository _contactsRepository;
        private readonly IEmailSender _emailSender;

        public ContactsBusinessLogic(IContactsRepository repo, IEmailSender emailSender)
        {
            _contactsRepository = repo;
            _emailSender = emailSender;
        }

        public async Task<ContactDTO> Add(ContactDTO contactDto)
        {
            Contact contactToAdd = ConvertDtoToModel(contactDto);

            return ConvertModelToDto(await _contactsRepository.Add(contactToAdd).ConfigureAwait(false));
        }

        public Contact ConvertDtoToModel(ContactDTO dto)
        {
            Contact model = new Contact();
            model.Id = dto.Id;
            model.CreatedUtc = dto.CreatedUtc;
            model.CellNumber = dto.CellNumber;
            model.OfficeNumber = dto.OfficeNumber;
            model.IsPrimary = dto.IsPrimary;
            model.IsAdmin = dto.IsAdmin;
            model.Name = dto.FullName;
            model.Email = dto.Email;
            model.Address = dto.Address.ConvertDtoToModel();
            model.CompanyId = dto.CompanyId;

            return model;
        }

        public ContactDTO ConvertModelToDto(Contact model)
        {
            var dto = new ContactDTO();
            dto.CellNumber = model.CellNumber;
            dto.OfficeNumber = model.OfficeNumber;
            dto.IsPrimary = model.IsPrimary;
            dto.IsAdmin = model.IsAdmin;
            dto.Id = model.Id;
            dto.CreatedUtc = model.CreatedUtc;
            dto.Email = model.Email;
            dto.CompanyId = model.CompanyId;
            dto.Address = model.Address.ConvertModelToDto();
            dto.FirstName = model.Name.Split(' ').First();
            dto.LastName = model.Name.Split(' ').Last();

            return dto;
        }

        public async Task<ContactDTO> Get(Guid contactGuid)
        {
            Contact contact = await _contactsRepository.Get(contactGuid).ConfigureAwait(false);

            return ConvertModelToDto(contact);
        }

        public async Task<PagingResultDTO<ContactDTO>> Get(ContactFilter filter)
        {
            PageResult<Contact> contacts = await _contactsRepository.Get(filter).ConfigureAwait(false);

            return contacts.PageResultToDto(ConvertModelToDto);
        }

        public void SendContactConsultantEmail(string clientEmail, string message)
        {
            _emailSender.EmailConsultants(message, "New request for your help through HireRight!");
        }

        public async Task SendNewContactEmail(Guid contactId, string message)
        {
            ContactDTO contact = await Get(contactId);

            _emailSender.EmailConsultants(message, contact.FullName + " has requested your help!", contact.Email);
        }

        public async Task<ContactDTO> Update(ContactDTO contactDto)
        {
            Contact contactToUpdate = ConvertDtoToModel(contactDto);

            return ConvertModelToDto(await _contactsRepository.Update(contactToUpdate).ConfigureAwait(false));
        }
    }
}