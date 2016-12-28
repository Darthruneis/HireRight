﻿using System;
using System.Runtime.Serialization;

namespace DataTransferObjects.Data_Transfer_Objects
{
    [Serializable]
    public class CategoryDTO : DataTransferObjectBase
    {
        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public CategoryImportance Importance { get; set; }

        [DataMember]
        public string Title { get; set; }

        public CategoryDTO()
        {
            Importance = CategoryImportance.Irrelevant;
        }

        public CategoryDTO(string title, string description)
        {
            Title = title;
            Description = description;
        }
    }
}