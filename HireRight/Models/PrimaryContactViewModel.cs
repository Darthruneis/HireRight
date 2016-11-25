using DataTransferObjects.Data_Transfer_Objects;
using System.ComponentModel.DataAnnotations;

namespace HireRight.Models
{
    public class PrimaryContactViewModel
    {
        [Required]
        public AddressDTO Address { get; set; }

        [Required]
        [Display(Name = "Personal Phone")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(500, MinimumLength = 2)]
        public string CellPhone { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(500, MinimumLength = 2)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(500, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(500, MinimumLength = 2)]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Office Phone")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(500, MinimumLength = 2)]
        public string OfficePhone { get; set; }

        public PrimaryContactViewModel()
        {
            Address = new AddressDTO();
        }

        public ContactDTO ConvertToContactDTO()
        {
            ContactDTO dto = new ContactDTO();

            dto.FirstName = FirstName;
            dto.LastName = LastName;
            dto.Address = Address;
            dto.Email = Email;
            dto.CellNumber = CellPhone;
            dto.OfficeNumber = OfficePhone;

            return dto;
        }
    }
}