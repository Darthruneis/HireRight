using DataTransferObjects.Data_Transfer_Objects;
using HireRight.BusinessLogic.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DataTransferObjects.Filters.Concrete;

namespace HireRight.API.Controllers
{
    [RoutePrefix("api/Categories")]
    public class CategoriesController : ApiControllerBase<CategoryDTO>
    {
        private readonly ICategoriesBusinessLogic _categoriesBusinessLogic;
        private readonly Func<CategoryFilter, Task<List<CategoryDTO>>> _getPage;

        public CategoriesController(ICategoriesBusinessLogic bll)
        {
            _categoriesBusinessLogic = bll;

            _add = _categoriesBusinessLogic.Add;
            _update = _categoriesBusinessLogic.Update;
            _getSingle = _categoriesBusinessLogic.Get;
            _getPage = _categoriesBusinessLogic.Get;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> AddCategory(CategoryDTO categoryToAdd)
        {
            return await AddBase(categoryToAdd);
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetCategories([FromUri] CategoryFilter filter)
        {
            return await GetMultipleBase(_getPage(filter));
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetCategory(Guid categoryGuid)
        {
            return await GetSingleBase(categoryGuid);
        }

        [HttpPut]
        public async Task<HttpResponseMessage> UpdateCategory(CategoryDTO categoryToUpdate)
        {
            return await UpdateBase(categoryToUpdate);
        }
    }
}