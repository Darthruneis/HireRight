using DataTransferObjects;
using DataTransferObjects.Data_Transfer_Objects;
using SDK.Abstract;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace SDK.Concrete
{
    public sealed class ApiSDKClient<TDto> : IApiSDKClient<TDto>
        where TDto : DataTransferObjectBase
    {
        public Uri BaseAddress { get; set; }

        private HttpClient Client { get; }

        public ApiSDKClient()
        {
            Client = new HttpClient();
        }

        public async Task<ApiResponse<TDto>> GetAsync(string query)
        {
            HttpRequestMessage request = CreateGetRequest(query);

            return await ReadApiResponse(request);
        }

        public async Task<ApiResponse<TDto>> PostAsync(TDto content)
        {
            HttpRequestMessage request = CreateRequest(HttpMethod.Post, string.Empty, content);

            return await ReadApiResponse(request);
        }

        public async Task<ApiResponse<TDto>> PutAsync(TDto content)
        {
            HttpRequestMessage request = CreateRequest(HttpMethod.Put, string.Empty, content);

            return await ReadApiResponse(request);
        }

        private HttpRequestMessage CreateGetRequest(string query = null)
        {
            return new HttpRequestMessage(HttpMethod.Get, BaseAddress + query);
        }

        private HttpRequestMessage CreateRequest(HttpMethod method, string query, TDto content = null)
        {
            if (method == HttpMethod.Get)
                return CreateGetRequest(query);

            HttpRequestMessage request = new HttpRequestMessage(method, BaseAddress + query);

            HttpContent httpContent = new ObjectContent(typeof(TDto), content, new JsonMediaTypeFormatter(), "application/json");

            request.Content = httpContent;

            return request;
        }

        private async Task<ApiResponse<TDto>> ReadApiResponse(HttpRequestMessage request)
        {
            HttpResponseMessage response = await SendAsync(request);

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