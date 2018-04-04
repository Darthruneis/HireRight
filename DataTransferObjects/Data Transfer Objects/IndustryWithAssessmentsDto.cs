using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DataTransferObjects.Data_Transfer_Objects
{
    public class IndustryWithAssessmentsDto : DataTransferObjectBase
    {
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public List<string> Assessments { get; set; }
    }
}