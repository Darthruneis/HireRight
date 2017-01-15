using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DataTransferObjects.Data_Transfer_Objects
{
    [Serializable]
    public class SubmitCardsDTO
    {
        [DataMember]
        public IList<CategoryDTO> Categories { get; set; }

        [DataMember]
        public string CompanyName { get; set; }

        [DataMember]
        public ContactDTO Contact { get; set; }

        [DataMember]
        public string Notes { get; set; }

        [DataMember]
        public IList<string> Positions { get; set; }

        public SubmitCardsDTO()
        {
            Categories = new List<CategoryDTO>();
            Positions = new List<string>();
            Contact = new ContactDTO();
        }
    }
}