using DataTransferObjects;
using DataTransferObjects.Data_Transfer_Objects;
using HireRight.API.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace HireRight.API.Handlers
{
    public class ApiResponseWrappingHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

                ObjectContent content = response.Content as ObjectContent;

                if (content == null)
                    return response;

                if (content.ObjectType.FullName.Contains(typeof(ApiResponse<>).FullName))
                    return response;

                if (content.ObjectType.FullName.Equals(typeof(decimal).FullName))
                    return response;

                throw new ApplicationException("Unsupported response type: " + content.ObjectType.FullName);
            }
            catch (Exception exception)
            {
                ApiResponse<DataTransferObjectBase> errorResponse = new ApiResponse<DataTransferObjectBase>(HttpStatusCode.BadRequest, exception.Message);

                return request.CreateResponse(HttpStatusCode.BadRequest, errorResponse);
            }
        }
    }
}