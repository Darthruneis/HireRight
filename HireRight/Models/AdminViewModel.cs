using DataTransferObjects.Data_Transfer_Objects;
using System.ComponentModel.DataAnnotations;

namespace HireRight.Models
{
    public class AdminViewModel
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public AdminViewModel()
        {
        }

        public ContactDTO ConvertToContactDTO()
        {
            ContactDTO dto = new ContactDTO();

            dto.FirstName = FirstName;
            dto.LastName = LastName;
            dto.Email = Email;
            dto.CellNumber = PhoneNumber;

            return dto;
        }
    }
}