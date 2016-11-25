using DataTransferObjects.Data_Transfer_Objects;
using System.ComponentModel.DataAnnotations;

namespace HireRight.Models
{
    public class ContactUsViewModel : PrimaryContactViewModel
    {
        [Required]
        [Display(Name = "Company")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Please tell us how we can be of assistance!")]
        [Display(Name = "Please tell us how we can be of assistance")]
        public string Message { get; set; }

        [Required]
        [Display(Name = "Position at Company")]
        public string PositionAtCompany { get; set; }

        public ContactUsDTO ConvertToContactUsDTO()
        {
            ContactUsDTO dto = new ContactUsDTO();

            dto.Message = Message;
            dto.FirstName = FirstName;
            dto.LastName = LastName;
            dto.Email = Email;
            dto.Address = Address;
            dto.CellPhone = CellPhone;
            dto.OfficePhone = OfficePhone;

            return dto;
        }
    }
}