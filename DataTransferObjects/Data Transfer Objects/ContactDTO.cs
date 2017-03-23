using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace DataTransferObjects.Data_Transfer_Objects
{
    [Serializable]
    public class ContactDTO : DataTransferObjectBase
    {
        [DataMember]
        public AddressDTO Address { get; set; }

        [DataMember]
        [Display(Name = "Personal Phone")]
        public string CellNumber { get; set; }

        public Guid CompanyId { get; set; }

        [DataMember]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [DataMember]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [DataMember]
        public string FullName => FirstName + " " + LastName;

        [DataMember]
        public bool IsAdmin { get; set; }

        [DataMember]
        public bool IsPrimary { get; set; }

        [DataMember]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [DataMember]
        [Display(Name = "Office Phone")]
        public string OfficeNumber { get; set; }

        public ContactDTO()
        {
            Address = new AddressDTO();
        }
    }
}