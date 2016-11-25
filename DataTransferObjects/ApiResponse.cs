using DataTransferObjects.Data_Transfer_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;

namespace DataTransferObjects
{
    [DataContract]
    public class ApiResponse<TDto>
        where TDto : DataTransferObjectBase
    {
        [DataMember]
        public string ErrorMessage { get; private set; }

        [DataMember]
        public IEnumerable<TDto> Results { get; private set; }

        [DataMember]
        public HttpStatusCode StatusCode { get; }

        [DataMember]
        public string StatusCodeReason => StatusCode.ToString();

        [DataMember]
        public uint TotalRecords { get; private set; }

        public ApiResponse(HttpStatusCode statusCode, uint totalRecordCount, params TDto[] results)
        {
            StatusCode = statusCode;
            TotalRecords = totalRecordCount;
            Results = results?.ToList();
            ErrorMessage = null;
        }

        public ApiResponse(HttpStatusCode statusCode = HttpStatusCode.BadRequest, string errorMessage = "Sorry, something went wrong while processing your request.")
            : this(statusCode, 0, null)
        {
            StatusCode = StatusCode;
            ErrorMessage = errorMessage;
        }

        public ApiResponse()
        {
            TotalRecords = 0;
            ErrorMessage = "Empty Response!";
            Results = new List<TDto>();
        }
    }
}