using DataTransferObjects.Data_Transfer_Objects;
using HireRight.Persistence.Models.CompanyAggregate;

namespace HireRight.BusinessLogic.Extensions
{
    public static class OrderConversionExtensions
    {
        public static Order ConvertDtoToModel(this OrderDetailsDTO dto)
        {
            Order model = new Order();

            model.Notes = dto.NotesAndPositions.Notes;
            model.PositionsOfInterest = dto.NotesAndPositions.PositionsOfInterest;
            model.Quantity = dto.Quantity;
            model.Status = OrderStatus.Pending;
            model.RowGuid = dto.RowGuid;
            model.CreatedUtc = dto.CreatedUtc;

            return model;
        }

        public static OrderDetailsDTO ConvertModelToDto(this Order model)
        {
            OrderDetailsDTO dto = new OrderDetailsDTO();
            dto.NotesAndPositions.Notes = model.Notes;
            dto.NotesAndPositions.PositionsOfInterest = model.PositionsOfInterest;
            dto.Quantity = model.Quantity;
            dto.RowGuid = model.RowGuid;
            dto.CreatedUtc = model.CreatedUtc;

            return dto;
        }
    }
}