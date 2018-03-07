using System;
using System.Runtime.Serialization;

namespace DataTransferObjects.Data_Transfer_Objects
{
    [Serializable]
    public class IndustryDTO
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public long Id { get; set; }

        private IndustryDTO() { }

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public IndustryDTO(string name, long id)
        {
            Name = name;
            Id = id;
        }
    }
}