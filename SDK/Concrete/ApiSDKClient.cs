using DataTransferObjects;
using SDK.Abstract;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Runtime.Remoting;
using System.Threading.Tasks;

namespace SDK.Concrete
{
    public sealed class ApiSDKClient : IApiSDKClient
    {
        public Uri BaseAddress { get; set; }

        private HttpClient Client { get; }

        public ApiSDKClient()
        {
            Client = new HttpClient();
        }

        public async Task<ApiResponse<TDto>> GetAsync<TDto>(string query)
        {
            HttpRequestMessage request = CreateGetRequest(query);

            return await ReadApiResponse<TDto>(request);
        }

        public async Task<ApiResponse<TDto>> PostAsync<TDto>(TDto content)
        {
            HttpRequestMessage request = CreateRequest(HttpMethod.Post, string.Empty, content);

            return await ReadApiResponse<TDto>(request);
        }

        public async Task<ApiResponse<TDto>> PostAsync<TDto>(IList<TDto> content)
        {
            HttpRequestMessage request = CreateRequest(HttpMethod.Post, string.Empty, content);

            return await ReadApiResponse<TDto>(request);
        }

        public async Task<ApiResponse<TDto>> PutAsync<TDto>(TDto content)
        {
            HttpRequestMessage request = CreateRequest(HttpMethod.Put, string.Empty, content);

            return await ReadApiResponse<TDto>(request);
        }

        private HttpRequestMessage CreateGetRequest(string query = null)
        {
            return new HttpRequestMessage(HttpMethod.Get, BaseAddress + query);
        }

        private HttpRequestMessage CreateRequest<TDto>(HttpMethod method, string query, TDto content)
        {
            if (method == HttpMethod.Get)
                return CreateGetRequest(query);

            HttpRequestMessage request = new HttpRequestMessage(method, BaseAddress + query);

            HttpContent httpContent = new ObjectContent(typeof(TDto), content, new JsonMediaTypeFormatter(), "application/json");

            request.Content = httpContent;

            return request;
        }

        private async Task<ApiResponse<TDto>> ReadApiResponse<TDto>(HttpRequestMessage request)
        {
            HttpResponseMessage response = await SendAsync(request);

            string test = await response.Content.ReadAsStringAsync();

            try
            {
                ApiResponse<TDto> apiResponse = await response.Content.ReadAsAsync<ApiResponse<TDto>>();

                return apiResponse;
            }
            catch (Exception ex)
            {
                throw new ServerException("Server did not respond with the expected object type.", ex);
            }
        }

        private async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
            => await Client.SendAsync(request);
    }
}