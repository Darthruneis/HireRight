using System.Runtime.Serialization;
using System.Xml.Serialization;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DataTransferObjects.Filters.Concrete
{
    [DataContract]
    public class PagingResultDTO<T> : ResultDTO
        where T : class
    {
        [DataMember]
        public long PageNumber { get; set; }

        [DataMember]
        public ICollection<T> PageResult { get; set; }

        [DataMember]
        public long PageSize { get; set; }

        [DataMember]
        public long TotalMatchingResults { get; set; }
    }

    [DataContract]
    public class ResultDTO
    {
        [DataMember]
        public string Error { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public bool IsFailure => !IsSuccess;

        [DataMember]
        public bool IsSuccess { get; set; }
    }

    [DataContract]
    public class ResultDTO<T> : ResultDTO
        where T : class
    {
        [DataMember]
        public T Results { get; set; }
    }
}