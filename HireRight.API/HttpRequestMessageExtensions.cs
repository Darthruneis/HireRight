using System.Net.Http;
using DataTransferObjects;
using DataTransferObjects.Data_Transfer_Objects;
using HireRight.API.Models;

namespace HireRight.API
{
    public static class HttpRequestMessageExtensions
    {
        public static HttpResponseMessage CreateResponse<TDto>(this HttpRequestMessage request, ApiResponse<TDto> response)
            where TDto : DataTransferObjectBase
        {
            return request.CreateResponse(response.StatusCode, response);
        }
    }
}