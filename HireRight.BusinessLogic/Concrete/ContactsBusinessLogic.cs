using DataTransferObjects.Data_Transfer_Objects;
using HireRight.BusinessLogic.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObjects.Filters.Concrete;
using HireRight.BusinessLogic.Extensions;
using HireRight.Persistence.Abstract;
using HireRight.Persistence.Models;
using HireRight.Persistence.Models.CompanyAggregate;

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

        public Task<IList<string>> SendNewClientEmail(ClientDTO newClient, string additionalInformation = "")
        {
            IList<string> errors = new List<string>();

            if(!(newClient.ToReceiveSample || newClient.ToScheduleDemo || newClient.ToTakeSampleAssesment || newClient.ToTalkToConsultant))
                errors.Add("Please select at least one of the options.");

            if(errors.Any())
                return Task.FromResult(errors);

            StringBuilder message = new StringBuilder();
            message.AppendLine($"{newClient.Name} is interested in using HireRight!  {newClient.Name} is a {newClient.CompanyPosition} at {newClient.Company}. <br/>");

            message.AppendLine("<ul>");

            if(newClient.ToTalkToConsultant)
                message.AppendLine("<li>They would like to contact a consultant directly at their earliest convenience.</li>");
            if (newClient.ToReceiveSample)
                message.AppendLine("<li>They are interested in receiving a sample assessment to evaluate.</li>");
            if (newClient.ToScheduleDemo)
                message.AppendLine("<li>They are interested in scheduling a demo.</li>");
            if (newClient.ToTakeSampleAssesment)
                message.AppendLine("<li>They are interested in taking a sample assessment.</li>");

            message.Append("</ul>");
            message.AppendLine("<br/>");

            if (!string.IsNullOrWhiteSpace(additionalInformation))
            {
                message.AppendLine("The client has included the following information:")
                       .AppendLine("<br/>")
                       .AppendLine(additionalInformation.Replace(Environment.NewLine, "<br/>"));
            }

            message.AppendLine("<br/>").AppendLine("Please contact the client as soon as possible.");

            SendContactConsultantEmail(newClient.Email, message.ToString());

            return Task.FromResult(errors);
        }

        public Contact ConvertDtoToModel(ContactDTO dto)
        {
            Contact model = new Contact();
            model.RowGuid = dto.RowGuid;
            model.CreatedUtc = dto.CreatedUtc;
            model.CellNumber = dto.CellNumber;
            model.OfficeNumber = dto.OfficeNumber;
            model.IsPrimary = dto.IsPrimary;
            model.IsAdmin = dto.IsAdmin;
            model.Name = dto.FullName;
            model.Email = dto.Email;
            model.Address = dto.Address.ConvertDtoToModel();

            return model;
        }

        public ContactDTO ConvertModelToDto(Contact model)
        {
            var dto = new ContactDTO();
            dto.CellNumber = model.CellNumber;
            dto.OfficeNumber = model.OfficeNumber;
            dto.IsPrimary = model.IsPrimary;
            dto.IsAdmin = model.IsAdmin;
            dto.RowGuid = model.RowGuid;
            dto.CreatedUtc = model.CreatedUtc;
            dto.Email = model.Email;
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
            _emailSender.EmailConsultants(message, "New request for your help through HireRight!", clientEmail);
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