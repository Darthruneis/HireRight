using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace DataTransferObjects.Data_Transfer_Objects
{
    [Serializable]
    public class ClientDTO : DataTransferObjectBase
    {
        [DataMember]
        public string Company { get; set; }

        [DataMember]
        public string CompanyPosition { get; set; }

        [DataMember]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [DataMember]
        public bool ToReceiveSample { get; set; }

        [DataMember]
        public bool ToScheduleDemo { get; set; }

        [DataMember]
        public bool ToTakeSampleAssesment { get; set; }

        [DataMember]
        public bool ToTalkToConsultant { get; set; }
    }
}