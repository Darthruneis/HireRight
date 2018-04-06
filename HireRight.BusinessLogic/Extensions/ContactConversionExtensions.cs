using System.Linq;
using DataTransferObjects.Data_Transfer_Objects;
using HireRight.Persistence.Models.CompanyAggregate;

namespace HireRight.BusinessLogic.Extensions
{
    public static class ContactConversionExtensions
    {
        public static Contact ConvertDtoToModel(this ContactDTO dto)
        {
            Contact model = new Contact();
            model.Email = dto.Email;
            model.Name = dto.FullName;
            model.CellNumber = dto.CellNumber;
            model.OfficeNumber = dto.OfficeNumber;
            model.IsPrimary = dto.IsPrimary;
            model.IsAdmin = dto.IsAdmin;

            try
            {
                model.Address = dto.Address.ConvertDtoToModel();
            }
            catch
            {
                model.Address = new Address(dto.Address.Country, null, null, null, "00000", null);
            }

            return model;
        }

        public static ContactDTO ConvertModelToDto(this Contact model)
        {
            ContactDTO dto = new ContactDTO();
            dto.Address = model.Address.ConvertModelToDto();
            dto.Email = model.Email;
            dto.FirstName = model.Name.Split(' ').First();
            dto.LastName = model.Name.Split(' ').Last();
            dto.CellNumber = model.CellNumber;
            dto.OfficeNumber = model.OfficeNumber;
            dto.IsAdmin = model.IsAdmin;
            dto.IsPrimary = model.IsPrimary;

            return dto;
        }
    }
}