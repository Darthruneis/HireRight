﻿using DataTransferObjects;
using DataTransferObjects.Data_Transfer_Objects;
using DataTransferObjects.Filters.Concrete;
using SDK.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;

namespace SDK.Concrete
{
    public class ContactsSDK : IContactsSDK
    {
        private readonly IApiSDKClient _client;

        public ContactsSDK(IApiSDKClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri(ConfigurationConstants.ApiUrl + typeof(ContactsSDK).Name.Replace("SDK", string.Empty));
        }

        public async Task<ContactDTO> AddContact(ContactDTO contactToAdd)
        {
            ApiResponse<ContactDTO> response = await _client.PostAsync(contactToAdd);

            return response.Results.First();
        }

        public async Task<ContactDTO> GetContact(Guid contactGuid)
        {
            ApiResponse<ContactDTO> response = await _client.GetAsync<ContactDTO>($"?{nameof(contactGuid)}={contactGuid}");

            return response.Results.First();
        }

        public async Task<List<ContactDTO>> GetContacts(ContactFilter filter)
        {
            ApiResponse<ContactDTO> response = await _client.GetAsync<ContactDTO>(filter.CreateQuery());

            return response.Results.ToList();
        }

        public async Task SendContactConsultantEmail(string clientEmail, string message)
        {
            HttpClient httpClient = new HttpClient();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, _client.BaseAddress + "/ContactConsultant");

            request.Content = new ObjectContent(typeof(object), new { ClientEmail = clientEmail, Message = message }, new JsonMediaTypeFormatter(), "application/json");

            HttpResponseMessage response = await httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException("Failed to send email.");
        }

        public async Task SendNewContactEmail(Guid contactId, string message)
        {
            HttpClient httpClient = new HttpClient();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, _client.BaseAddress + "/NewContact");

            request.Content = new ObjectContent(typeof(object), new { Id = contactId, Message = message }, new JsonMediaTypeFormatter(), "application/json");

            HttpResponseMessage response = await httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException("Failed to send email.");
        }

        public async Task<ContactDTO> UpdateContact(ContactDTO contactToUpdate)
        {
            ApiResponse<ContactDTO> response = await _client.PutAsync(contactToUpdate);

            return response.Results.First();
        }
    }
}