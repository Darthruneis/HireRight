using DataTransferObjects.Data_Transfer_Objects;
using DataTransferObjects.Filters;
using HireRight.BusinessLogic.Abstract;
using HireRight.EntityFramework.CodeFirst.Models;
using HireRight.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using DataTransferObjects.Filters.Concrete;
using HireRight.EntityFramework.CodeFirst.Models.OrderAggregate;

namespace HireRight.BusinessLogic.Concrete
{
    public class ContactsBusinessLogic : IContactsBusinessLogic
    {
        private readonly IContactsRepository _contactsRepository;
        private readonly SmtpClient _emailClient;

        public ContactsBusinessLogic(IContactsRepository repo)
        {
            _contactsRepository = repo;
            _emailClient = new SmtpClient();

            _emailClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
            _emailClient.DeliveryFormat = SmtpDeliveryFormat.SevenBit;
            _emailClient.PickupDirectoryLocation = @"C:\Users\Chris\Desktop\HireRight Test Emails";
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

            return model;
        }

        public ContactDTO ConvertModelToDto(Contact model)
        {
            ContactDTO dto = new ContactDTO();
            dto.CellNumber = model.CellNumber;
            dto.OfficeNumber = model.OfficeNumber;
            dto.IsPrimary = model.IsPrimary;
            dto.IsAdmin = model.IsAdmin;
            dto.Id = model.Id;
            dto.CreatedUtc = model.CreatedUtc;

            return dto;
        }

        public async Task<ContactDTO> Get(Guid contactGuid)
        {
            Contact contact = await _contactsRepository.Get(contactGuid).ConfigureAwait(false);

            return ConvertModelToDto(contact);
        }

        public async Task<List<ContactDTO>> Get(ContactFilter filter)
        {
            List<Contact> contacts = await _contactsRepository.Get(filter).ConfigureAwait(false);

            return contacts.Select(ConvertModelToDto).ToList();
        }

        public async Task SendContactConsultantEmail(string clientEmail, string message)
        {
            await EmailBothConsultants(clientEmail, "New request for your help through HireRight!", message);
        }

        public async Task SendNewContactEmail(Guid contactId, string message)
        {
            ContactDTO contact = await Get(contactId);

            await EmailBothConsultants(contact.Email, contact.FullName + " has requested your help!", message);
        }

        public async Task<ContactDTO> Update(ContactDTO contactDto)
        {
            Contact contactToUpdate = ConvertDtoToModel(contactDto);

            return ConvertModelToDto(await _contactsRepository.Update(contactToUpdate).ConfigureAwait(false));
        }

        private MailMessage CreateMailMessageForSmtpClient(string to, string from, string subject, string message)
        {
            MailMessage mailMessage = new MailMessage(to, from, subject, message);
            mailMessage.ReplyToList.Add(from);

            return mailMessage;
        }

        private async Task EmailBothConsultants(string from, string subject, string message)
        {
            await _emailClient.SendMailAsync(CreateMailMessageForSmtpClient("silverasoc@aol.com", from, subject, message));
            await _emailClient.SendMailAsync(CreateMailMessageForSmtpClient("janet@something.com", from, subject, message));
        }
    }
}