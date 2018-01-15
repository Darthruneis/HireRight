using DataTransferObjects.Data_Transfer_Objects;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HireRight.Models
{
    public class NewClientsViewModel
    {
        public string AdditionalInfo { get; set; }
        public string Company { get; set; }

        [Display(Name = "Position at Company")]
        public string CompanyPosition { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Name { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        public bool ToReceiveSample { get; set; }
        public bool ToScheduleDemo { get; set; }
        public bool ToTakeSampleAssesment { get; set; }
        public bool ToTalkToConsultant { get; set; }

        public ClientDTO ConvertToClientDTO()
        {
            ClientDTO dto = new ClientDTO();

            dto.Name = Name;
            dto.Phone = Phone;
            dto.Email = Email;

            dto.ToTalkToConsultant = ToTalkToConsultant;
            dto.ToTakeSampleAssesment = ToTakeSampleAssesment;
            dto.ToScheduleDemo = ToScheduleDemo;
            dto.ToReceiveSample = ToReceiveSample;

            dto.Company = Company;
            dto.CompanyPosition = CompanyPosition;

            return dto;
        }

        public bool AnyOptionsSelected => ToReceiveSample || ToScheduleDemo || ToTakeSampleAssesment || ToTalkToConsultant;

        public ContactDTO ConvertToContactDTO()
        {
            ContactDTO dto = new ContactDTO();

            string[] names = Name.Split(' ');
            dto.FirstName = names.First();
            dto.LastName = names.Last();
            dto.CellNumber = Phone;
            dto.Email = Email;

            return dto;
        }
    }
}