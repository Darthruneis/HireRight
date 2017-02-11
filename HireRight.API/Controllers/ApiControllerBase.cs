using DataTransferObjects;
using DataTransferObjects.Data_Transfer_Objects;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace HireRight.API.Controllers
{
    public class ApiControllerBase<TDto> : ApiController
        where TDto : DataTransferObjectBase
    {
        protected Func<TDto, Task<TDto>> _add;
        protected Func<Guid, Task<TDto>> _getSingle;
        protected Func<TDto, Task<TDto>> _update;

        private const string NullInputErrorMessage = "Dto must not be null!";

        protected async Task<HttpResponseMessage> AddBase(TDto itemToAdd)
        {
            try
            {
                if (itemToAdd == null)
                    return Request.CreateResponse(new ApiResponse<TDto>(HttpStatusCode.BadRequest, NullInputErrorMessage));

                TDto result = await _add(itemToAdd);

                return Request.CreateResponse(new ApiResponse<TDto>(HttpStatusCode.Created, 1, result));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected HttpResponseMessage CreateResponse(params TDto[] results)
        {
            return Request.CreateResponse(new ApiResponse<TDto>(HttpStatusCode.OK, (uint)results.Length, results));
        }

        protected async Task<HttpResponseMessage> GetMultipleBase(Task<List<TDto>> getMultipleTask)
        {
            try
            {
                var items = await getMultipleTask;

                return CreateResponse(items.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected async Task<HttpResponseMessage> GetSingleBase(Guid itemGuid)
        {
            try
            {
                TDto result = await _getSingle(itemGuid);

                if (result == null)
                    return Request.CreateResponse(new ApiResponse<TDto>(HttpStatusCode.NotFound, "We were unable to find the item that was requested."));

                return Request.CreateResponse(new ApiResponse<TDto>(HttpStatusCode.OK, 1, result));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected async Task<HttpResponseMessage> UpdateBase(TDto itemToUpdate)
        {
            try
            {
                if (itemToUpdate == null)
                    return Request.CreateResponse(new ApiResponse<TDto>(HttpStatusCode.BadRequest, NullInputErrorMessage));

                TDto result = await _update(itemToUpdate);

                return Request.CreateResponse(new ApiResponse<TDto>(HttpStatusCode.OK, 1, result));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}