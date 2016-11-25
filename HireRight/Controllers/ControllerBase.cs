using DataTransferObjects.Data_Transfer_Objects;
using SDK.Abstract;
using System.Web.Mvc;

namespace HireRight.Controllers
{
    public abstract class ControllerBase<TDto> : Controller
        where TDto : DataTransferObjectBase
    {
    }
}