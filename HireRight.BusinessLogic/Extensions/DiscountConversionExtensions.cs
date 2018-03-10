using DataTransferObjects.Data_Transfer_Objects;
using HireRight.EntityFramework.CodeFirst.Models.CompanyAggregate;

namespace HireRight.BusinessLogic.Extensions
{
    public static class DiscountConversionExtensions
    {
        public static Discount ConvertDtoToModel(this DiscountDTO dto)
        {
            Discount model = new Discount(dto.IsPercent, dto.Amount, dto.Threshold);
            model.RowGuid = dto.RowGuid;
            model.CreatedUtc = dto.CreatedUtc;

            return model;
        }

        public static DiscountDTO ConvertModelToDto(this Discount model)
        {
            DiscountDTO dto = new DiscountDTO();
            dto.RowGuid = model.RowGuid;
            dto.CreatedUtc = model.CreatedUtc;
            dto.Amount = model.Amount;
            dto.IsPercent = model.IsPercent;
            dto.Threshold = model.Threshold;

            return dto;
        }
    }
}