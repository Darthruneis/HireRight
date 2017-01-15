using System.ComponentModel.DataAnnotations;
using DataTransferObjects.Data_Transfer_Objects;

namespace HireRight.Models
{
    public class CompanyViewModel
    {
        [Required]
        public AddressDTO CompanyAddress { get; set; }

        [Required]
        [Display(Name = "Position at Company")]
        [StringLength(500, MinimumLength = 2)]
        public string CompanyPosition { get; set; }

        [Required]
        [Display(Name = "Company Name")]
        [StringLength(500, MinimumLength = 2)]
        public string CompanyTitle { get; set; }

        public CompanyViewModel()
        {
            CompanyAddress = new AddressDTO();
        }

        public CompanyDTO ConvertToCompanyDTO()
        {
            CompanyDTO dto = new CompanyDTO();

            dto.Name = CompanyTitle;
            dto.BillingAddress = CompanyAddress;

            return dto;
        }
    }
}