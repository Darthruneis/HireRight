using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DataTransferObjects.Data_Transfer_Objects
{
    [Serializable]
    public class CategoryDTO : DataTransferObjectBase
    {
        [DataMember]
        public ContactDTO Contact { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public NotesPositionsDTO Details { get; set; }

        [DataMember]
        public CategoryImportance Importance { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public List<long> Industries { get; set; }

        public CategoryDTO()
        {
            Importance = CategoryImportance.Irrelevant;
            Contact = new ContactDTO();
            Details = new NotesPositionsDTO();
        }

        public CategoryDTO(string title, string description) : this()
        {
            Title = title;
            Description = description;
        }
    }
}