using DataTransferObjects.Data_Transfer_Objects;
using HireRight.EntityFramework.CodeFirst.Models.CompanyAggregate;

namespace HireRight.BusinessLogic.Extensions
{
    public static class LocationConversionExtensions
    {
        public static CompanyLocation ConvertDtoToModel(this LocationDTO dto)
        {
            CompanyLocation model = new CompanyLocation();
            model.Address = dto.Address.ConvertDtoToModel();
            model.Description = dto.Description;
            model.Label = dto.Label;

            return model;
        }

        public static LocationDTO ConvertModelToDto(this CompanyLocation model)
        {
            LocationDTO dto = new LocationDTO(model);

            return dto;
        }
    }
}