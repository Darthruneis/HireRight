using DataTransferObjects.Data_Transfer_Objects;
using HireRight.Persistence.Models.Abstract;

namespace HireRight.BusinessLogic.Abstract
{
    public interface IBusinessLogicBase<TModel, TDto>
        where TModel : PocoBase
        where TDto : DataTransferObjectBase
    {
        TModel ConvertDtoToModel(TDto dto);

        TDto ConvertModelToDto(TModel model);
    }
}